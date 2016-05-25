using KAS.Trukman.AppContext;
using KAS.Trukman.Classes;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KAS.Trukman.ViewModels.Pages.Owner
{
    #region OwnerDelayAlertsViewModel
    public class OwnerDelayAlertsViewModel : PageViewModel
    {
        public OwnerDelayAlertsViewModel()
            : base()
        {
            this.JobAlerts = new ObservableCollection<JobAlert>();

            this.SelectJobAlertCommand = new VisualCommand(this.SelectJobAlert);
            this.RefreshCommand = new VisualCommand(this.Refresh);
            this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);
            this.ShowMainMenuCommand = new VisualCommand(this.ShowMainMenu);
        }

        public override void Appering()
        {
            base.Appering();

            this.SelectJobAlerts();
        }

        public override void Disappering()
        {
            base.Disappering();
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.OwnerBrokerListPageName;
        }

        private void SelectJobAlerts()
        {
            Task.Run(async() => {
                this.IsBusy = true;
                try
                {
                    var jobAlerts = await TrukmanContext.SelectJobAlertsAsync();
                    this.ShowJobAlerts(jobAlerts);
                }
                catch (Exception exception)
                {
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    this.IsRefreshing = false;
                    this.IsBusy = false;
                }
            });
        }

        private void ShowJobAlerts(JobAlert[] jobAlerts)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.JobAlerts.Clear();
                this.SelectedJobAlert = null;

                foreach (var jobAlert in jobAlerts)
                    this.JobAlerts.Add(jobAlert);
            });
        }

        private void ShowMainMenu(object parameter)
        {
            ShowMainMenuMessage.Send();
        }

        private void ShowHomePage(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        private void SelectJobAlert(object parameter)
        {
            this.SelectedJobAlert = null;
        }

        private void Refresh(object parameter)
        {
            this.SelectJobAlerts();
        }

        public ObservableCollection<JobAlert> JobAlerts { get; private set; }

        public JobAlert SelectedJobAlert
        {
            get { return (this.GetValue("SelectedJobAlert") as JobAlert); }
            set { this.SetValue("SelectedJobAlert", value); }
        }

        public bool IsRefreshing
        {
            get { return (bool)this.GetValue("IsRefreshing", false); }
            set { this.SetValue("IsRefreshing", value); }
        }

        public VisualCommand SelectJobAlertCommand { get; private set; }

        public VisualCommand ShowMainMenuCommand { get; private set; }

        public VisualCommand ShowHomePageCommand { get; private set; }

        public VisualCommand RefreshCommand { get; private set; }
    }
    #endregion
}
