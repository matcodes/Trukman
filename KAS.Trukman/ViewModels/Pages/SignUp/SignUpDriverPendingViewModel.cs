using KAS.Trukman.Classes;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Data.Enums;
using KAS.Trukman.AppContext;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Trukman.ViewModels.Pages.SignUp
{
    #region SignUpDriverPendingViewModel
    public class SignUpDriverPendingViewModel : PageViewModel
    {
        private System.Timers.Timer _checkDriverStateTimer = null;

        public SignUpDriverPendingViewModel()
            : base()
        {
            this.EnglishLanguageCommand = new VisualCommand(this.EnglishLanguage);
            this.EspanolLanguageCommand = new VisualCommand(this.EspanolLanguage);
            this.ShowPrevPageCommand = new VisualCommand(this.ShowPrevPage);
            this.CancelCommand = new VisualCommand(this.Cancel);
        }

        public override void Appering()
        {
            this.StartCheckDriverStatusTimer();

            base.Appering();
        }

        public override void Disappering()
        {
            this.StopCheckDriverStatusTimer();

            base.Disappering();
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.SignUpPageName;

            this.MessageText = String.Format(AppLanguages.CurrentLanguage.SignUpDriverPendingLabel, (this.Company != null ? this.Company.DisplayName : ""));
        }

        protected override void DoPropertyChanged(string propertyName)
        {
            if (propertyName == "Company")
                this.Localize();

            base.DoPropertyChanged(propertyName);
        }

        public override void Initialize(params object[] parameters)
        {
            this.Company = ((parameters != null && parameters.Length > 0) ? (parameters[0] as Company) : null);
        }

        private void StartCheckDriverStatusTimer()
        {
            if (_checkDriverStateTimer == null)
            {
                _checkDriverStateTimer = new System.Timers.Timer { Interval = 15000 };
                _checkDriverStateTimer.Elapsed += (sender, args) =>
                {
                    this.CheckDriverStatus();
                };
            }
            _checkDriverStateTimer.Start();
        }

        private void CheckDriverStatus()
        {
            Task.Run(async () =>
            {
                this.StopCheckDriverStatusTimer();
                this.IsBusy = true;
                try
                {
                    var driverState = await TrukmanContext.GetDriverState();
                    if (driverState == DriverState.Joined)
                        ShowSignUpDriverAuthorizedPageMessage.Send(this.Company);
                    else if (driverState == DriverState.Declined)
                        ShowSignUpDriverDeclinedPageMessage.Send(this.Company);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    this.IsBusy = false;
                    this.StartCheckDriverStatusTimer();
                }
            });
        }

        private void StopCheckDriverStatusTimer()
        {
            if (_checkDriverStateTimer != null)
                _checkDriverStateTimer.Stop();
        }

        private void ShowPrevPage(object parameter)
        {
            PopPageMessage.Send();
        }

        private void EnglishLanguage(object parameter)
        {
            this.SelectedLanguage = SignUpLanguage.English;
        }

        private void EspanolLanguage(object parameter)
        {
            this.SelectedLanguage = SignUpLanguage.Espanol;
        }

        private void Cancel(object parameter)
        {
            Task.Run(async () => {
                this.IsBusy = true;
                try
                {
                    await TrukmanContext.CancelDriverRequest();
                    ShowTopPageMessage.Send();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    this.IsBusy = false;
                }
            });
        }

        public SignUpLanguage SelectedLanguage
        {
            get { return (SignUpLanguage)this.GetValue("SelectedLanguage", SignUpLanguage.English); }
            set { this.SetValue("SelectedLanguage", value); }
        }

        public string MessageText
        {
            get { return (string)this.GetValue("MessageText"); }
            set { this.SetValue("MessageText", value); }
        }

        public Company Company
        {
            get { return (this.GetValue("Company") as Company); }
            set { this.SetValue("Company", value); }
        }

        public VisualCommand ShowPrevPageCommand { get; private set; }

        public VisualCommand EnglishLanguageCommand { get; private set; }

        public VisualCommand EspanolLanguageCommand { get; private set; }

        public VisualCommand CancelCommand { get; private set; }
    }
    #endregion
}
