using System;
//using Trukman.Languages;
using Xamarin.Forms;
using Trukman.Messages;
using KAS.Trukman.ViewModels.Pages;
using KAS.Trukman.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using KAS.Trukman.Data.Interfaces;
using KAS.Trukman.Droid.AppContext;
using System.Collections.ObjectModel;

namespace Trukman.ViewModels.Pages
{
    #region SignUpDriverViewModel
    public class SignUpDriverViewModel : PageViewModel
	{
        private System.Timers.Timer _selectCompaniesTimer;

		public SignUpDriverViewModel () : base()
		{
            this.Companies = new ObservableCollection<ICompany>();

			this.ShowPrevPageCommand = new VisualCommand (this.ShowPrevPage);
            this.SelectCompanyCommand = new VisualCommand(this.SelectCompany);
            this.SelectCompanyAcceptCommand = new VisualCommand(this.SelectCompanyAccept);
            this.SelectCompanyCancelCommand = new VisualCommand(this.SelectCompanyCancel);
		}

		protected override void Localize ()
		{
			base.Localize ();

			this.Title = AppLanguages.CurrentLanguage.SignUpLabel.ToUpper ();
		}

		protected override void DisableCommands()
		{
			base.DisableCommands();

			Device.BeginInvokeOnMainThread(() => 
				{
					this.ShowPrevPageCommand.IsEnabled = false;
				});
		}

		protected override void EnabledCommands()
		{
			base.EnabledCommands();

			Device.BeginInvokeOnMainThread(() => 
				{
					this.ShowPrevPageCommand.IsEnabled = true;
				});
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

        private void StopSelectCompaniesTimer()
        {
            if (_selectCompaniesTimer != null)
                _selectCompaniesTimer.Stop();
        }

        private void ShowPrevPage(object parameter)
		{
			PopPageMessage.Send();
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

        public ObservableCollection<ICompany> Companies { get; private set; }

        public ICompany SelectedCompany
        {
            get { return (this.GetValue("SelectedCompany") as ICompany); }
            set { this.SetValue("SelectedCompany", value); }
        }

        public string CompanyFilter
        {
            get { return (string)this.GetValue("CompanyFilter"); }
            set { this.SetValue("CompanyFilter", value); }
        }

        public bool SelectCompanyPopupVisible
        {
            get { return (bool)this.GetValue("SelectCompanyPopupVisible", false); }
            set { this.SetValue("SelectCompanyPopupVisible", value); }
        }

        public string CompanyName
        {
            get { return (string)this.GetValue("CompanyName"); }
            set { this.SetValue("CompanyName", value); }
        }

        public VisualCommand PopPageCommand { get; private set; }

        public VisualCommand SelectCompanyCommand { get; private set; }

        public VisualCommand SelectCompanyAcceptCommand { get; private set; }

        public VisualCommand SelectCompanyCancelCommand { get; private set; }

        public VisualCommand ShowPrevPageCommand { get; private set; }
	}
    #endregion
}

