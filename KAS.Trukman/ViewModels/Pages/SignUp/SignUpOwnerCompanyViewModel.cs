using KAS.Trukman.Classes;
using KAS.Trukman.Data.Enums;
using KAS.Trukman.Data.Infos;
using KAS.Trukman.AppContext;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trukman.Helpers;

namespace KAS.Trukman.ViewModels.Pages.SignUp
{
    #region SignUpOwnerCompanyViewModel
    public class SignUpOwnerCompanyViewModel : PageViewModel
    {
        public SignUpOwnerCompanyViewModel()
            : base()
        {
            this.EnglishLanguageCommand = new VisualCommand(this.EnglishLanguage);
            this.EspanolLanguageCommand = new VisualCommand(this.EspanolLanguage);
            this.ShowPrevPageCommand = new VisualCommand(this.ShowPrevPage);
            this.SubmitCommand = new VisualCommand(this.Submit);
        }

        public override void Initialize(params object[] parameters)
        {
            var info = (parameters.Length > 0 ? (parameters[0] as MCInfo) : null);

            this.Name = (info != null ? info.Name : "");
            this.MCCode = (info != null ? info.MCCode : "");
            this.DBA = (info != null ? info.DBA : "");
            this.Address = (info != null ? info.Address : "");
            this.Phone = (info != null ? info.Phone : "");
            this.EMail = "";
            this.FleetSize = "";
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
            Task.Run(async () => {
                this.IsBusy = true;
                try
                {
                    var emailTester = new Regex("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");
                    var fleetTester = new Regex("^+([0-9])+$");

                    if (String.IsNullOrEmpty(this.Phone))
                        throw new Exception(AppLanguages.CurrentLanguage.SignUpCompanyPhoneEmptyErrorMessageText);
                    else if ((String.IsNullOrEmpty(this.EMail)) || (!emailTester.IsMatch(this.EMail.Trim())))
                        throw new Exception(AppLanguages.CurrentLanguage.SignUpCompanyIncorectEMailErrorMessageText);
                    else if ((String.IsNullOrEmpty(this.FleetSize)) || (!fleetTester.IsMatch(this.FleetSize.Trim())))
                        throw new Exception(AppLanguages.CurrentLanguage.SignUpCompanyFleetSizeErrorMessageText);

                    int fleetSize = 0;
                    int.TryParse(this.FleetSize, out fleetSize);

                    var companyInfo = new CompanyInfo
                    {
                        Name = this.Name,
                        MCCode = this.MCCode,
                        DBA = this.DBA,
                        Address = this.Address,
                        Phone = this.Phone,
                        EMail = this.EMail,
                        FleetSize = fleetSize
                    };

                    var company = await TrukmanContext.RegisterCompanyAsync(companyInfo);

                    ShowSignUpOwnerWelcomePageMessage.Send(company);
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

        public string Name
        {
            get { return (string)this.GetValue("Name"); }
            set { this.SetValue("Name", value); }
        }

        public string MCCode
        {
            get { return (string)this.GetValue("MCCode"); }
            set { this.SetValue("MCCode", value); }
        }

        public string DBA
        {
            get { return (string)this.GetValue("DBA"); }
            set { this.SetValue("DBA", value); }
        }

        public string Address
        {
            get { return (string)this.GetValue("Address"); }
            set { this.SetValue("Address", value); }
        }

        public string Phone
        {
            get { return (string)this.GetValue("Phone"); }
            set { this.SetValue("Phone", value); }
        }

        public string EMail
        {
            get { return (string)this.GetValue("EMail"); }
            set { this.SetValue("EMail", value); }
        }

        public string FleetSize
        {
            get { return (string)this.GetValue("FleetSize"); }
            set { this.SetValue("FleetSize", value); }
        }

        public VisualCommand ShowPrevPageCommand { get; private set; }

        public VisualCommand EnglishLanguageCommand { get; private set; }

        public VisualCommand EspanolLanguageCommand { get; private set; }

        public VisualCommand SubmitCommand { get; private set; }
    }
    #endregion
}
