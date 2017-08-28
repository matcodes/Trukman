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
using KAS.Trukman.Extensions;

namespace KAS.Trukman.ViewModels.Pages.SignUp
{
    #region SignUpOwnerCompanyViewModel
    public class SignUpOwnerCompanyViewModel : PageViewModel
    {
        private System.Timers.Timer _showTimer;

        public SignUpOwnerCompanyViewModel() : base()
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

        protected override void DoPropertyChanged(string propertyName)
        {
            base.DoPropertyChanged(propertyName);

            if (propertyName == "EnterConfirmationCodePopupVisible")
            {
                if (!this.EnterConfirmationCodePopupVisible)
                {
                    this.StopShowTimer();
                    this.ConfirmationState = 0;
                }
            }
            else if (propertyName == "ConfirmationCodeAcceptedPopupVisible")
            {
                if (this.ConfirmationCodeAcceptedPopupVisible)
                    this.EnterConfirmationCodePopupVisible = false;
            }
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
            Task.Run(async () =>
            {
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
            }).LogExceptions("SignUpOwnerCompanyViewModel Submit");
        }

        private void StartShowTimer()
        {
            _showTimer = new System.Timers.Timer { Interval = 10000 };
            _showTimer.Elapsed += (sender, args) =>
            {
                this.StopShowTimer();
                this.ConfirmationState = 0;
            };
            _showTimer.Start();
        }

        private void StopShowTimer()
        {
            if (_showTimer != null)
                _showTimer.Stop();
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
                        this.StopShowTimer();
                        this.ConfirmationState = 2;
                        this.StartShowTimer();
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
            }).LogExceptions("SignUpOwnerCompanyViewModel SubmitCode");
        }

        public void CancelConfirmationCode(object parameter)
        {
            this.EnterConfirmationCodePopupVisible = false;
        }

        public void ResendConfirmationCode(object parameter)
        {
            if (this.ConfirmationState != 1)
            {
                Task.Run(async () =>
                {
                    this.IsBusy = true;
                    try
                    {
                        await TrukmanContext.ResendVerificationCode();

                        this.StopShowTimer();
                        this.ConfirmationState = 1;
                        this.StartShowTimer();
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
                }).LogExceptions("SignUpOwnerCompanyViewModel ResendConfirmationCode");
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
            }).LogExceptions("SignUpOwnerCompanyViewModel Continue");
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

        public int ConfirmationState
        {
            get { return (int)this.GetValue("ConfirmationState", 0); }
            set { this.SetValue("ConfirmationState", value); }
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
