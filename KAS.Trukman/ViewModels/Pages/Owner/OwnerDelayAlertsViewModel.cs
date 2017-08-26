using KAS.Trukman.AppContext;
using KAS.Trukman.Classes;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using KAS.Trukman.Extensions;

namespace KAS.Trukman.ViewModels.Pages.Owner
{
    #region OwnerDelayAlertsViewModel
    public class OwnerDelayAlertsViewModel : PageViewModel
    {
        public OwnerDelayAlertsViewModel() : base()
        {
            this.JobAlertGroups = new ObservableCollection<JobAlertGroup>();

            this.SelectJobAlertCommand = new VisualCommand(this.SelectJobAlert);
            this.RefreshCommand = new VisualCommand(this.Refresh);
            this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);
            this.ShowMainMenuCommand = new VisualCommand(this.ShowMainMenu);
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
            base.Initialize(parameters);

            this.SelectJobAlerts();
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.OwnerDelayAlertsPageName;
        }

        private void SelectJobAlerts()
        {
            Task.Run(async () =>
            {
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
            }).LogExceptions("OwnerDelayAlertsViewModel SelectJobAlerts");
        }

        private void ShowJobAlerts(JobAlert[] jobAlerts)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.JobAlertGroups.Clear();
                this.SelectedJobAlert = null;

                foreach (var jobAlert in jobAlerts)
                {
                    var group = this.JobAlertGroups.FirstOrDefault(jag => jag.Job.ID == jobAlert.Job.ID);
                    if (group == null)
                    {
                        group = new JobAlertGroup(jobAlert.Job);
                        this.JobAlertGroups.Add(group);
                    }
                    group.Add(jobAlert);
                }
            });
        }

        private void RemoveJobAlert(JobAlert jobAlert)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var group = this.JobAlertGroups.FirstOrDefault(jag => jag.Job.ID == jobAlert.Job.ID);
                if (group != null)
                {
                    group.Remove(jobAlert);
                    if (group.Count == 0)
                        this.JobAlertGroups.Remove(group);
                }
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
            Task.Run(async () =>
            {
                this.IsBusy = true;
                try
                {
                    var jobAlert = (parameter as JobAlert);
                    if (jobAlert != null)
                    {
                        await TrukmanContext.SetJobAlertIsViewedAsync(jobAlert.ID, true);
                        this.RemoveJobAlert(jobAlert);
                    }
                }
                catch (Exception exception)
                {
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    this.SelectedJobAlert = null;
                    this.IsBusy = false;
                }
            }).LogExceptions("OwnerDelayAlertsViewModel SelectJobAlert");
        }

        private void Refresh(object parameter)
        {
            this.SelectJobAlerts();
        }

        public ObservableCollection<JobAlertGroup> JobAlertGroups { get; private set; }

        public JobAlert SelectedJobAlert
        {
            get { return (this.GetValue("SelectedJobPoint") as JobAlert); }
            set { this.SetValue("SelectedJobPoint", value); }
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

    #region JobAlertGroup
    public class JobAlertGroup : ObservableCollection<JobAlert>
    {
        public JobAlertGroup(Trip job)
            : base()
        {
            this.Job = job;
        }

        public Trip Job { get; private set; }
    }
    #endregion
}
