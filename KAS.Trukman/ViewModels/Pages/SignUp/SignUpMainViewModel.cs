using KAS.Trukman.Classes;
using KAS.Trukman.Data.Enums;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.ViewModels.Pages.SignUp
{
    #region SignUpMainViewModel
    public class SignUpMainViewModel : PageViewModel
    {
        public SignUpMainViewModel()
            : base()
        {
            this.EnglishLanguageCommand = new VisualCommand(this.EnglishLanguage);
            this.EspanolLanguageCommand = new VisualCommand(this.EspanolLanguage);
            this.DriverSignUpCommand = new VisualCommand(this.DriverSignUp);
            this.DispatcherSignUpCommand = new VisualCommand(this.DispatcherSignUp);
            this.OwnerSignUpCommand = new VisualCommand(this.OwnerSignUp);
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

            this.Title = AppLanguages.CurrentLanguage.AppName;
        }

        private void EnglishLanguage(object parameter)
        {
            this.SelectedLanguage = SignUpLanguage.English;
        }

        private void EspanolLanguage(object parameter)
        {
            this.SelectedLanguage = SignUpLanguage.Espanol;
        }

        private void DriverSignUp(object parameter)
        {
            this.SelectedContext = SignUpContext.Driver;
            ShowSignUpDriverPageMessage.Send();
        }

        private void DispatcherSignUp(object parameter)
        {
            this.SelectedContext = SignUpContext.Dispatcher;
            ShowSignUpDispatcherPageMessage.Send();
        }

        private void OwnerSignUp(object parameter)
        {
            this.SelectedContext = SignUpContext.Owner;
            ShowSignUpOwnerMCPageMessage.Send();
        }

        public SignUpLanguage SelectedLanguage
        {
            get { return (SignUpLanguage)this.GetValue("SelectedLanguage", SignUpLanguage.English); }
            set { this.SetValue("SelectedLanguage", value); }
        }

        public SignUpContext SelectedContext
        {
            get { return (SignUpContext)this.GetValue("SelectedContext", SignUpContext.None); }
            set { this.SetValue("SelectedContext", value); }
        }

        public VisualCommand EnglishLanguageCommand { get; private set; }

        public VisualCommand EspanolLanguageCommand { get; private set; }

        public VisualCommand DriverSignUpCommand { get; private set; }

        public VisualCommand DispatcherSignUpCommand { get; private set; }

        public VisualCommand OwnerSignUpCommand { get; private set; }
    }
    #endregion
}
