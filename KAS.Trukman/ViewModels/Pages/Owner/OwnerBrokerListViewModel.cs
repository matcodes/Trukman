using KAS.Trukman.AppContext;
using KAS.Trukman.Classes;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Extensions;
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
    #region OwnerBrokerListViewModel
    public class OwnerBrokerListViewModel : PageViewModel
    {
        public OwnerBrokerListViewModel() : base()
        {
            this.Brokers = new ObservableCollection<User>();

            this.SelectBrokerCommand = new VisualCommand(this.SelectBroker);
            this.RefreshCommand = new VisualCommand(this.Refresh);
            this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);
            this.ShowMainMenuCommand = new VisualCommand(this.ShowMainMenu);
        }

        public override void Appering()
        {
            base.Appering();

            this.SelectBrokers();
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

        private void SelectBrokers()
        {
            Task.Run(async () =>
            {
                this.IsBusy = true;
                try
                {
                    var brokers = await TrukmanContext.SelectBrockersAsync();
                    this.ShowBrokers(brokers);
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
            }).LogExceptions("OwnerBrokerListViewModel SelectBrokers");
        }

        private void ShowBrokers(User[] brokers)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.Brokers.Clear();
                this.SelectedBroker = null;

                foreach (var brocker in brokers)
                    this.Brokers.Add(brocker);
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

        private void SelectBroker(object parameter)
        {
            this.SelectedBroker = null;
        }

        private void Refresh(object parameter)
        {
            this.SelectBrokers();
        }

        public ObservableCollection<User> Brokers { get; private set; }

        public User SelectedBroker
        {
            get { return (this.GetValue("SelectedBroker") as User); }
            set { this.SetValue("SelectedBroker", value); }
        }

        public bool IsRefreshing
        {
            get { return (bool)this.GetValue("IsRefreshing", false); }
            set { this.SetValue("IsRefreshing", value); }
        }

        public VisualCommand SelectBrokerCommand { get; private set; }

        public VisualCommand ShowMainMenuCommand { get; private set; }

        public VisualCommand ShowHomePageCommand { get; private set; }

        public VisualCommand RefreshCommand { get; private set; }
    }
    #endregion
}
