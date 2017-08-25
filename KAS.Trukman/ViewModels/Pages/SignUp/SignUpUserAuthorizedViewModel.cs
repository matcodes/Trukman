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
    #region SignUpUserAuthorizedViewModel
    public class SignUpUserAuthorizedViewModel : PageViewModel
    {
        public SignUpUserAuthorizedViewModel() : base()
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

        public override void Initialize(params object[] parameters)
        {
            this.Company = (parameters != null && parameters.Length > 0 ? (parameters[0] as Company) : null);
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.SignUpPageName;

            this.MessageText = String.Format(AppLanguages.CurrentLanguage.SignUpUserAuthorizedLabel, (this.Company != null ? this.Company.DisplayName : ""));
        }

        protected override void DoPropertyChanged(string propertyName)
        {
            if (propertyName == "Company")
                this.Localize();

            base.DoPropertyChanged(propertyName);
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
            Task.Run(async () =>
            {
                this.IsBusy = true;
                try
                {
                    if (TrukmanContext.User.Role == UserRole.Driver)
                        await TrukmanContext.InitializeDriverContext();
                    if (TrukmanContext.User.Role == UserRole.Dispatch)
                        await TrukmanContext.InitializeDispatcherContext();
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

        public VisualCommand ContinueCommand { get; private set; }
    }
    #endregion
}
