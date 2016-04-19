using KAS.Trukman.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KAS.Trukman.ViewModels.Pages
{
    #region OwnerSignUpWelcomeViewModel
    public class OwnerSignUpWelcomeViewModel : PageViewModel
    {
        private System.Timers.Timer _checkDriversTimer = null;

        public OwnerSignUpWelcomeViewModel() 
            : base()
        {
            this.PopPageCommand = new VisualCommand(this.PopPage);
            this.ContinueCommand = new VisualCommand(this.Continue);
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);

            this.CompanyName = ((parameters != null) && (parameters.Length > 0) ? parameters[0].ToString() : "");
        }

        public override void Appering()
        {
            base.Appering();

            this.CheckDrivers();
        }

        public override void Disappering()
        {
            this.StopCheckDriversTimer();

            base.Disappering();
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.SignUpLabel;
            this.WelcomeText = String.Format(AppLanguages.CurrentLanguage.SignUpOwnerWelcomeLabel, this.CompanyName);
        }

        protected override void DoPropertyChanged(string propertyName)
        {
            if (propertyName == "CompanyName")
                this.Localize();

            base.DoPropertyChanged(propertyName);
        }

        private void StartCheckDriversTimer()
        {
            if (_checkDriversTimer == null)
            {
                _checkDriversTimer = new System.Timers.Timer { Interval = 10000 };
                _checkDriversTimer.Elapsed += (sender, args) => {
                    this.CheckDrivers();
                };
            }
            _checkDriversTimer.Start();
        }

        private void StopCheckDriversTimer()
        {
            if (_checkDriversTimer != null)
                _checkDriversTimer.Stop();
        }

        private void CheckDrivers()
        {
            Task.Run(() => {
                this.IsBusy = true;
                this.DisableCommands();
                try
                {
                    Thread.Sleep(2000);
                    ShowDriverAuthorizationPageMessage.Send(null);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    // To do: Show exception message
                }
                finally
                {
                    this.EnabledCommands();
                    this.IsBusy = false;
                }

                this.StartCheckDriversTimer();
            });
        }

        private void PopPage(object parameter)
        {
            PopPageMessage.Send();
        }

        private void Continue(object parameter)
        {
            
        }

        public string CompanyName
        {
            get { return (string)this.GetValue("CompanyName"); }
            set { this.SetValue("CompanyName", value); }
        }

        public string WelcomeText
        {
            get { return (string)this.GetValue("WelcomeText"); }
            set { this.SetValue("WelcomeText", value); }
        }

        public VisualCommand PopPageCommand { get; private set; }

        public VisualCommand ContinueCommand { get; private set; }
    }
    #endregion
}
