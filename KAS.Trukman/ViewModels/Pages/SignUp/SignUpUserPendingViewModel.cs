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
    #region SignUpUserPendingViewModel
    public class SignUpUserPendingViewModel : PageViewModel
    {
        private System.Timers.Timer _checkUserStateTimer = null;

        public SignUpUserPendingViewModel() : base()
        {
            this.EnglishLanguageCommand = new VisualCommand(this.EnglishLanguage);
            this.EspanolLanguageCommand = new VisualCommand(this.EspanolLanguage);
            this.ShowPrevPageCommand = new VisualCommand(this.ShowPrevPage);
            this.CancelCommand = new VisualCommand(this.Cancel);
        }

        public override void Appering()
        {
            this.StartCheckUserStatusTimer();

            base.Appering();
        }

        public override void Disappering()
        {
            this.StopCheckUserStatusTimer();

            base.Disappering();
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.SignUpPageName;

            this.MessageText = String.Format(AppLanguages.CurrentLanguage.SignUpUserPendingLabel, (this.Company != null ? this.Company.DisplayName : ""));
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

            if (TrukmanContext.User.Role == Data.Enums.UserRole.Dispatch)
                this.UserRole = AppLanguages.CurrentLanguage.SignUpDispatcherLabel;
            else if (TrukmanContext.User.Role == Data.Enums.UserRole.Driver)
                this.UserRole = AppLanguages.CurrentLanguage.SignUpDriverLabel;
        }

        private void StartCheckUserStatusTimer()
        {
            if (_checkUserStateTimer == null)
            {
                _checkUserStateTimer = new System.Timers.Timer { Interval = 15000 };
                _checkUserStateTimer.Elapsed += (sender, args) =>
                {
                    this.CheckUserStatus();
                };
            }
            _checkUserStateTimer.Start();
        }

        private void CheckUserStatus()
        {
            Task.Run(async () =>
            {
                this.StopCheckUserStatusTimer();
                this.IsBusy = true;
                try
                {
                    var userState = await TrukmanContext.GetUserState();
                    if (userState == UserState.Joined)
                        ShowSignUpUserAuthorizedPageMessage.Send(this.Company);
                    else if (userState == UserState.Declined)
                        ShowSignUpUserDeclinedPageMessage.Send(this.Company);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    this.IsBusy = false;
                    this.StartCheckUserStatusTimer();
                }
            });
        }

        private void StopCheckUserStatusTimer()
        {
            if (_checkUserStateTimer != null)
                _checkUserStateTimer.Stop();
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
                    await TrukmanContext.CancelUserRequest();
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

        public string UserRole
        {
            get { return (string)this.GetValue("UserRole", ""); }
            set { this.SetValue("UserRole", value); }
        }

        public VisualCommand ShowPrevPageCommand { get; private set; }

        public VisualCommand EnglishLanguageCommand { get; private set; }

        public VisualCommand EspanolLanguageCommand { get; private set; }

        public VisualCommand CancelCommand { get; private set; }
    }
    #endregion
}
