using KAS.Trukman.Classes;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Data.Enums;
using KAS.Trukman.Droid.AppContext;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Trukman.ViewModels.Pages.SignUp
{
    #region SignUpOwnerWelcomeViewModel
    public class SignUpOwnerWelcomeViewModel : PageViewModel
    {
        public SignUpOwnerWelcomeViewModel()
            : base()
        {
            this.EnglishLanguageCommand = new VisualCommand(this.EnglishLanguage);
            this.EspanolLanguageCommand = new VisualCommand(this.EspanolLanguage);
            this.ShowPrevPageCommand = new VisualCommand(this.ShowPrevPage);
            this.ContinueCommand = new VisualCommand(this.Continue);
        }

        public override void Appering()
        {
            base.Appering();
        }

        public override void Disappering()
        {
            base.Disappering();
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.SignUpPageName;

            this.WelcomeText = String.Format(AppLanguages.CurrentLanguage.SignUpOwnerWelcomeLabel, (this.Company != null ? this.Company.DisplayName : ""));
        }

        protected override void DoPropertyChanged(string propertyName)
        {
            if (propertyName == "Company")
                this.Localize();

            base.DoPropertyChanged(propertyName);
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);

            this.Company = (parameters != null && parameters.Length > 0 ? (parameters[0] as Company) : null);
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

        private void Continue(object parameter)
        {
            Task.Run(async () => {
                this.IsBusy = true;
                try
                {
                    await TrukmanContext.InitializeOwnerContext();
                }
                catch (Exception exception)
                {
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

        public Company Company
        {
            get { return (this.GetValue("Company") as Company); }
            set { this.SetValue("Company", value); }
        }

        public string WelcomeText
        {
            get { return (string)this.GetValue("WelcomeText"); }
            set { this.SetValue("WelcomeText", value); }
        }

        public VisualCommand ShowPrevPageCommand { get; private set; }

        public VisualCommand EnglishLanguageCommand { get; private set; }

        public VisualCommand EspanolLanguageCommand { get; private set; }

        public VisualCommand ContinueCommand { get; private set; }
    }
    #endregion
}
