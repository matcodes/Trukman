using KAS.Trukman.Classes;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Data.Enums;
using KAS.Trukman.Data.Infos;
using KAS.Trukman.Data.Interfaces;
using KAS.Trukman.AppContext;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KAS.Trukman.ViewModels.Pages.SignUp
{
    #region SignUpDriverViewModel
    public class SignUpDriverViewModel : PageViewModel
    {
        private System.Timers.Timer _selectCompaniesTimer;

        public SignUpDriverViewModel()
            : base()
        {
            this.Companies = new ObservableCollection<ICompany>();

            this.EnglishLanguageCommand = new VisualCommand(this.EnglishLanguage);
            this.EspanolLanguageCommand = new VisualCommand(this.EspanolLanguage);
            this.ShowPrevPageCommand = new VisualCommand(this.ShowPrevPage);
            this.SubmitCommand = new VisualCommand(this.Submit);
            this.SelectCompanyCommand = new VisualCommand(this.SelectCompany);
            this.SelectCompanyAcceptCommand = new VisualCommand(this.SelectCompanyAccept);
            this.SelectCompanyCancelCommand = new VisualCommand(this.SelectCompanyCancel);
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
            if (propertyName == "CompanyFilter")
            {
                this.StopSelectCompaniesTimer();
                this.StartSelectCompaniesTimer();
            }

            base.DoPropertyChanged(propertyName);
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

		private void ShowCompanies(ICompany[] companies)
		{
			Device.BeginInvokeOnMainThread (() => {
				this.Companies.Clear();
				ICompany selected = null;
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
                    var driverInfo = new DriverInfo
                    {
                        FirstName = this.FirstName,
                        LastName = this.LastName,
                        Phone = this.Phone,
                        Company = this.SelectedCompany
                    };
                    var company = await TrukmanContext.RegisterDriverAsync(driverInfo);

					var state = (DriverState)TrukmanContext.User.Status; //await TrukmanContext.GetDriverState();
                    if (state == DriverState.Joined)
                        await TrukmanContext.InitializeDriverContext();
                    else if (state == DriverState.Waiting)
                        ShowSignUpDriverPendingPageMessage.Send(company);
                    else if (state == DriverState.Declined)
                        ShowSignUpDriverDeclinedPageMessage.Send(company);
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

        public string CompanyName
        {
            get { return (string)this.GetValue("CompanyName"); }
            set { this.SetValue("CompanyName", value); }
        }

        public ICompany SelectedCompany
        {
            get { return (this.GetValue("SelectedCompany") as ICompany); }
            set { this.SetValue("SelectedCompany", value); }
        }

        public bool SelectCompanyPopupVisible
        {
            get { return (bool)this.GetValue("SelectCompanyPopupVisible", false); }
            set { this.SetValue("SelectCompanyPopupVisible", value); }
        }

        public string CompanyFilter
        {
            get { return (string)this.GetValue("CompanyFilter"); }
            set { this.SetValue("CompanyFilter", value); }
        }

        public ObservableCollection<ICompany> Companies { get; private set; }

        public VisualCommand ShowPrevPageCommand { get; private set; }

        public VisualCommand EnglishLanguageCommand { get; private set; }

        public VisualCommand EspanolLanguageCommand { get; private set; }

        public VisualCommand SubmitCommand { get; private set; }

        public VisualCommand SelectCompanyCommand { get; private set; }

        public VisualCommand SelectCompanyAcceptCommand { get; private set; }

        public VisualCommand SelectCompanyCancelCommand { get; private set; }
    }
    #endregion
}
