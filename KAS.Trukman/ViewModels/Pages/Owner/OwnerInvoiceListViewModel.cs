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
    #region OwnerInvoiceListViewModel
    public class OwnerInvoiceListViewModel : PageViewModel
    {
        public OwnerInvoiceListViewModel()
            : base()
        {
            this.Jobs = new ObservableCollection<Trip>();

            this.SelectJobCommand = new VisualCommand(this.SelectJob);
            this.RefreshCommand = new VisualCommand(this.Refresh);
            this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);
            this.ShowMainMenuCommand = new VisualCommand(this.ShowMainMenu);
        }

        public override void Appering()
        {
            base.Appering();

            this.SelectJobs();
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

            this.Title = AppLanguages.CurrentLanguage.OwnerInvoiceListPageName;
        }

        private void SelectJobs()
        {
            Task.Run(async() => {
                this.IsBusy = true;
                try
                {
                    var jobs = await TrukmanContext.SelectCompletedTrips();
                    this.ShowBrokers(jobs);
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

        private void ShowBrokers(Trip[] jobs)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.Jobs.Clear();
                this.SelectedJob = null;

                foreach (var job in jobs)
                    this.Jobs.Add(job);
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

        private void SelectJob(object parameter)
        {
            this.SelectedJob = null;
        }

        private void Refresh(object parameter)
        {
            this.SelectJobs();
        }

        public ObservableCollection<Trip> Jobs { get; private set; }

        public Trip SelectedJob
        {
            get { return (this.GetValue("SelectedJob") as Trip); }
            set { this.SetValue("SelectedJob", value); }
        }

        public bool IsRefreshing
        {
            get { return (bool)this.GetValue("IsRefreshing", false); }
            set { this.SetValue("IsRefreshing", value); }
        }

        public VisualCommand SelectJobCommand { get; private set; }

        public VisualCommand ShowMainMenuCommand { get; private set; }

        public VisualCommand ShowHomePageCommand { get; private set; }

        public VisualCommand RefreshCommand { get; private set; }
    }
    #endregion
}
