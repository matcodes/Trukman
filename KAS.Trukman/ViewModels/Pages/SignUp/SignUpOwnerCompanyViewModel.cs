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
using System.Timers;

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
            this.SubmitCodeCommand = new VisualCommand(this.SubmitCode);
            this.CancelConfirmationCodeCommand = new VisualCommand(this.CancelConfirmationCode);
            this.ResendConfirmationCodeCommand = new VisualCommand(this.ResendConfirmationCode);
            this.ContinueCommand = new VisualCommand(this.Continue);
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
                    if (TrukmanContext.User.Verified)
                        ShowSignUpOwnerWelcomePageMessage.Send(company);
                    else
                        this.EnterConfirmationCodePopupVisible = true;
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


        public void SubmitCode(object parameter)
        {
            Task.Run(async () =>
            {
                this.IsBusy = true;
                try
                {
                    var verified = await TrukmanContext.Verification(this.ConfirmationCode);
                    if (verified)
                    {
                        this.EnterConfirmationCodePopupVisible = false;
                        this.ConfirmationCodeAcceptedPopupVisible = true;
                    }
                    else
                    {
                        this.ConfirmationCodeInvalidVisible = true;
                        Timer _timer = new Timer(10000);
                        _timer.Elapsed += ((sender, args) =>
                        {
                            this.ConfirmationCodeInvalidVisible = false;
                        });
                        _timer.Start();
                    }
                    this.ConfirmationCode = "";
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

        public void CancelConfirmationCode(object parameter)
        {
            this.EnterConfirmationCodePopupVisible = false;
            this.ConfirmationCodeSentVisible = false;
            this.ConfirmationCodeInvalidVisible = false;
            this.ConfirmationCodeAcceptedPopupVisible = false;
        }

        public void ResendConfirmationCode(object parameter)
        {
            if (!this.ConfirmationCodeSentVisible)
            {
                Task.Run(async () =>
                {
                    this.IsBusy = true;
                    try
                    {
                        await TrukmanContext.ResendVerificationCode();
                        this.ConfirmationCodeInvalidVisible = false;
                        this.ConfirmationCodeSentVisible = true;
                        Timer _timer = new Timer(10000);
                        _timer.Elapsed += ((sender, args) =>
                        {
                            this.ConfirmationCodeSentVisible = false;
                        });
                        _timer.Start();
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
        }

        public void Continue(object parameter)
        {
            Task.Run(() =>
            {
                this.IsBusy = true;
                try
                {
                    ShowSignUpOwnerWelcomePageMessage.Send(TrukmanContext.Company);
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

        public bool EnterConfirmationCodePopupVisible
        {
            get { return (bool)this.GetValue("EnterConfirmationCodePopupVisible", false); }
            set { this.SetValue("EnterConfirmationCodePopupVisible", value); }
        }

        public bool ConfirmationCodeSentVisible
        {
            get { return (bool)this.GetValue("ConfirmationCodeSentVisible", false); }
            set { this.SetValue("ConfirmationCodeSentVisible", value); }
        }

        public bool ConfirmationCodeInvalidVisible
        {
            get { return (bool)this.GetValue("ConfirmationCodeInvalidVisible", false); }
            set { this.SetValue("ConfirmationCodeInvalidVisible", value); }
        }

        public bool ConfirmationCodeAcceptedPopupVisible
        {
            get { return (bool)this.GetValue("ConfirmationCodeAcceptedPopupVisible", false); }
            set { this.SetValue("ConfirmationCodeAcceptedPopupVisible", value); }
        }

        public string ConfirmationCode
        {
            get { return (string)this.GetValue("ConfirmationCode"); }
            set { this.SetValue("ConfirmationCode", value); }
        }

        public VisualCommand ShowPrevPageCommand { get; private set; }

        public VisualCommand EnglishLanguageCommand { get; private set; }

        public VisualCommand EspanolLanguageCommand { get; private set; }

        public VisualCommand SubmitCommand { get; private set; }

        public VisualCommand SubmitCodeCommand { get; private set; }

        public VisualCommand CancelConfirmationCodeCommand { get; private set; }

        public VisualCommand ResendConfirmationCodeCommand { get; private set; }

        public VisualCommand ContinueCommand { get; private set; }
    }
    #endregion
}
