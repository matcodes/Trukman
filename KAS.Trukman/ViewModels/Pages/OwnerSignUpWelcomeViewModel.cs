using KAS.Trukman.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.ViewModels.Pages
{
    #region OwnerSignUpWelcomeViewModel
    public class OwnerSignUpWelcomeViewModel : PageViewModel
    {
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
        }

        public override void Disappering()
        {
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
