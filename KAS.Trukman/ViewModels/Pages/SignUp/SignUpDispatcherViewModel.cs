using KAS.Trukman.AppContext;
using KAS.Trukman.Classes;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Data.Enums;
using KAS.Trukman.Data.Infos;
using KAS.Trukman.Extensions;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;

namespace KAS.Trukman.ViewModels.Pages.SignUp
{
    #region SignUpDispatcherViewModel
    public class SignUpDispatcherViewModel : PageViewModel
    {
        private Timer _selectCompaniesTimer;
        private Timer _showTimer;

        public SignUpDispatcherViewModel() : base()
        {
            this.Companies = new ObservableCollection<Company>();

            this.EnglishLanguageCommand = new VisualCommand(this.EnglishLanguage);
            this.EspanolLanguageCommand = new VisualCommand(this.EspanolLanguage);
            this.ShowPrevPageCommand = new VisualCommand(this.ShowPrevPage);
            this.SubmitCommand = new VisualCommand(this.Submit);
            this.SelectCompanyCommand = new VisualCommand(this.SelectCompany);
            this.SelectCompanyAcceptCommand = new VisualCommand(this.SelectCompanyAccept);
            this.SelectCompanyCancelCommand = new VisualCommand(this.SelectCompanyCancel);
            this.SubmitCodeCommand = new VisualCommand(this.SubmitCode);
            this.CancelConfirmationCodeCommand = new VisualCommand(this.CancelConfirmationCode);
            this.ResendConfirmationCodeCommand = new VisualCommand(this.ResendConfirmationCode);
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
        }

        protected override void DoPropertyChanged(string propertyName)
        {
            base.DoPropertyChanged(propertyName);

            if (propertyName == "CompanyFilter")
            {
                this.StopSelectCompaniesTimer();
                this.StartSelectCompaniesTimer();
            }
            else if (propertyName == "EnterConfirmationCodePopupVisible")
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

        private void StartSelectCompaniesTimer()
        {
            if (_selectCompaniesTimer == null)
            {
                _selectCompaniesTimer = new System.Timers.Timer { Interval = 300 };
                _selectCompaniesTimer.Elapsed += async (sender, args) =>
                {
                    this.StopSelectCompaniesTimer();
                    this.IsBusy = true;
                    try
                    {
                        var companies = await TrukmanContext.SelectCompanies(this.CompanyFilter);
                        this.ShowCompanies(companies);
                    }
                    catch (Exception exception)
                    {
                        ShowToastMessage.Send(exception.Message);
                    }
                    finally
                    {
                        this.IsBusy = false;
                    }
                };
            }
            _selectCompaniesTimer.Start();
        }

        private void ShowCompanies(Company[] companies)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.Companies.Clear();
                Company selected = null;
                this.SelectedCompany = null;
                foreach (var company in companies)
                {
                    this.Companies.Add(company);
                    if (selected == null)
                    {
                        selected = company;
                        this.SelectedCompany = selected;
                    }
                }
            });
        }

        private void StopSelectCompaniesTimer()
        {
            if (_selectCompaniesTimer != null)
                _selectCompaniesTimer.Stop();
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
                    var dispatcherInfo = new DispatcherInfo
                    {
                        FirstName = this.FirstName,
                        LastName = this.LastName,
                        Phone = this.Phone,
                        Company = this.SelectedCompany,
                        EMail = this.EMail
                    };

                    await TrukmanContext.DispatcherLogin(dispatcherInfo);
                    if (TrukmanContext.User.Verified)
                        await RegisterDispatcher(dispatcherInfo);
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
            }).LogExceptions("SignUpDispatcherViewModel Submit");
        }

        private async Task RegisterDispatcher(DispatcherInfo dispatcherInfo)
        {
            var company = await TrukmanContext.RegisterDispatcherAsync(dispatcherInfo);

            var state = (UserState)TrukmanContext.User.Status;
            if (state == UserState.Joined)
                await TrukmanContext.InitializeDispatcherContext();
            else if (state == UserState.Waiting)
            {
                ShowSignUpUserPendingPageMessage.Send(company);
            }
            else if (state == UserState.Declined)
                ShowSignUpUserDeclinedPageMessage.Send(company);
        }

        public void SelectCompany(object parameter)
        {
            this.CompanyFilter = "";
            this.Companies.Clear();
            this.SelectCompanyPopupVisible = true;
        }

        public void SelectCompanyAccept(object parameter)
        {
            this.SelectCompanyPopupVisible = false;
            this.CompanyName = (this.SelectedCompany != null ? this.SelectedCompany.DisplayName : "");
        }

        public void SelectCompanyCancel(object parameter)
        {
            this.SelectedCompany = null;
            this.SelectCompanyPopupVisible = false;
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
                        this.ConfirmationCodeAcceptedPopupVisible = true;
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
            }).LogExceptions("SignUpDispatcherViewModel SubmitCode");
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
                }).LogExceptions("SignUpDispatcherViewModel ResendConfirmationCode");
            }
        }

        public void Continue(object parameter)
        {
            Task.Run(async () =>
            {
                this.IsBusy = true;
                try
                {
                    var dispatcherInfo = new DispatcherInfo
                    {
                        FirstName = this.FirstName,
                        LastName = this.LastName,
                        Phone = this.Phone,
                        Company = this.SelectedCompany,
                        EMail = this.EMail
                    };

                    this.ConfirmationCodeAcceptedPopupVisible = false;
                    await RegisterDispatcher(dispatcherInfo);
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
            }).LogExceptions("SignUpDispatcherViewModel Continue");
        }

        public SignUpLanguage SelectedLanguage
        {
            get { return (SignUpLanguage)this.GetValue("SelectedLanguage", SignUpLanguage.English); }
            set { this.SetValue("SelectedLanguage", value); }
        }

        public string FirstName
        {
            get { return (string)this.GetValue("FirstName"); }
            set { this.SetValue("FirstName", value); }
        }

        public string LastName
        {
            get { return (string)this.GetValue("LastName"); }
            set { this.SetValue("LastName", value); }
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

        public string CompanyName
        {
            get { return (string)this.GetValue("CompanyName"); }
            set { this.SetValue("CompanyName", value); }
        }

        public Company SelectedCompany
        {
            get { return (this.GetValue("SelectedCompany") as Company); }
            set { this.SetValue("SelectedCompany", value); }
        }

        public bool SelectCompanyPopupVisible
        {
            get { return (bool)this.GetValue("SelectCompanyPopupVisible", false); }
            set { this.SetValue("SelectCompanyPopupVisible", value); }
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

        public string CompanyFilter
        {
            get { return (string)this.GetValue("CompanyFilter"); }
            set { this.SetValue("CompanyFilter", value); }
        }

        public string ConfirmationCode
        {
            get { return (string)this.GetValue("ConfirmationCode"); }
            set { this.SetValue("ConfirmationCode", value); }
        }

        public ObservableCollection<Company> Companies { get; private set; }

        public VisualCommand ShowPrevPageCommand { get; private set; }

        public VisualCommand EnglishLanguageCommand { get; private set; }

        public VisualCommand EspanolLanguageCommand { get; private set; }

        public VisualCommand SubmitCommand { get; private set; }

        public VisualCommand SelectCompanyCommand { get; private set; }

        public VisualCommand SelectCompanyAcceptCommand { get; private set; }

        public VisualCommand SelectCompanyCancelCommand { get; private set; }

        public VisualCommand SubmitCodeCommand { get; private set; }

        public VisualCommand CancelConfirmationCodeCommand { get; private set; }

        public VisualCommand ResendConfirmationCodeCommand { get; private set; }

        public VisualCommand ContinueCommand { get; private set; }
    }
    #endregion
}
