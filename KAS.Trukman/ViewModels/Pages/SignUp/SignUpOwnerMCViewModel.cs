using KAS.Trukman.Classes;
using KAS.Trukman.Data.Enums;
using KAS.Trukman.Extensions;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trukman.Helpers;

namespace KAS.Trukman.ViewModels.Pages.SignUp
{
    #region SignUpOwnerMCViewModel
    public class SignUpOwnerMCViewModel : PageViewModel
    {
        private int _failedRequestCount = 0;

        public SignUpOwnerMCViewModel() : base()
        {
            this.EnglishLanguageCommand = new VisualCommand(this.EnglishLanguage);
            this.EspanolLanguageCommand = new VisualCommand(this.EspanolLanguage);
            this.ShowPrevPageCommand = new VisualCommand(this.ShowPrevPage);
            this.SubmitCommand = new VisualCommand(this.Submit);
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
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);

            this.MCCode = "";
        }

        protected override void DoPropertyChanged(string propertyName)
        {
            if (propertyName == "MCCode")
                this.SetSubmitEnabled();

            base.DoPropertyChanged(propertyName);
        }

        private void SetSubmitEnabled()
        {
            this.IsSubmitEnabled = (!string.IsNullOrEmpty(this.MCCode));
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

        private void Submit(object parameter)
        {
            Task.Run(async () =>
            {
                this.IsBusy = true;
                this.IsSubmitEnabled = false;
                try
                {
                    MCInfo mcInfo = await MCQuery.VerifyMC(this.MCCode);
                    if (mcInfo.Success)
                        ShowSignUpOwnerCompanyPageMessage.Send(mcInfo);
                    else
                        throw new Exception("Didn't found company with this MC");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);

                    _failedRequestCount++;
                    if (_failedRequestCount == 3)
                        this.PopupVisible = true;
                    else
                        ShowToastMessage.Send(AppLanguages.CurrentLanguage.SignUpMCNotFoundErrorMessageText);
                }
                finally
                {
                    this.IsSubmitEnabled = true;
                    this.IsBusy = false;
                }
            }).LogExceptions("SignUpOwnerMCViewModel Submit");
        }

        private void Continue(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        public string MCCode
        {
            get { return (string)this.GetValue("MCCode"); }
            set { this.SetValue("MCCode", value); }
        }

        public SignUpLanguage SelectedLanguage
        {
            get { return (SignUpLanguage)this.GetValue("SelectedLanguage", SignUpLanguage.English); }
            set { this.SetValue("SelectedLanguage", value); }
        }

        public bool PopupVisible
        {
            get { return (bool)this.GetValue("PopupVisible", false); }
            set { this.SetValue("PopupVisible", value); }
        }

        public bool IsSubmitEnabled
        {
            get { return (bool)this.GetValue("IsSubmitEnabled", true); }
            set { this.SetValue("IsSubmitEnabled", value); }
        }

        public VisualCommand ShowPrevPageCommand { get; private set; }

        public VisualCommand EnglishLanguageCommand { get; private set; }

        public VisualCommand EspanolLanguageCommand { get; private set; }

        public VisualCommand SubmitCommand { get; private set; }

        public VisualCommand ContinueCommand { get; private set; }
    }
    #endregion
}
