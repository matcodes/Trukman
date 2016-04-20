using KAS.Trukman.Classes;
using KAS.Trukman.Data.Interfaces;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Trukman.ViewModels.Pages
{
    #region MainMenuViewModel
    public class MainMenuViewModel : PageViewModel 
    {
        private ITrip _trip = null;

        public MainMenuViewModel() 
            : base()
        {
            this.SelectItemCommand = new VisualCommand(this.SelectItem);

            this.ShowHomePageMenuItem = new MenuItem(new VisualCommand(this.ShowHomePage));
            this.ShowTripPageMenuItem = new MenuItem(new VisualCommand(this.ShowTripPage));
            this.ShowAdvancesPageMenuItem = new MenuItem(new VisualCommand(this.ShowAdvancesPage));
            this.ShowDelayEmergencyPageMenuItem = new MenuItem(new VisualCommand(this.ShowDelayEmergencyPage));
            this.ShowRoutePageMenuItem = new MenuItem(new VisualCommand(this.ShowRoutePage));
            this.ShowPointsAndRewardsPageMenuItem = new MenuItem(new VisualCommand(this.ShowPointsAndRewardsPage));
            this.ShowSettingsPageMenuItem = new MenuItem(new VisualCommand(this.ShowSettingsPage));
            this.ShowHelpPageMenuItem = new MenuItem(new VisualCommand(this.ShowHelpPage));

            this.Items = new ObservableCollection<MenuItem>();
            this.Items.Add(this.ShowHomePageMenuItem);
            this.Items.Add(this.ShowTripPageMenuItem);
            this.Items.Add(this.ShowAdvancesPageMenuItem);
            this.Items.Add(this.ShowDelayEmergencyPageMenuItem);
            this.Items.Add(this.ShowRoutePageMenuItem);
            this.Items.Add(this.ShowPointsAndRewardsPageMenuItem);
            this.Items.Add(this.ShowSettingsPageMenuItem);
            this.Items.Add(this.ShowHelpPageMenuItem);

            this.Localize();

            this.TripChanged(null);

            TripChangedMessage.Subscribe(this, this.TripChanged);
        }

        protected override void Localize()
        {
            base.Localize();

            if (this.ShowHomePageMenuItem != null)
            {
                this.ShowHomePageMenuItem.Label = AppLanguages.CurrentLanguage.MainMenuHomeLabel;
                this.ShowTripPageMenuItem.Label = AppLanguages.CurrentLanguage.MainMenuTripLabel;
                this.ShowAdvancesPageMenuItem.Label = AppLanguages.CurrentLanguage.MainMenuAdvancesLabel;
                this.ShowDelayEmergencyPageMenuItem.Label = AppLanguages.CurrentLanguage.MainMenuDelayEmergencyLabel;
                this.ShowRoutePageMenuItem.Label = AppLanguages.CurrentLanguage.MainMenuRouteLabel;
                this.ShowPointsAndRewardsPageMenuItem.Label = AppLanguages.CurrentLanguage.MainMenuPointsAndRewardsLabel;
                this.ShowSettingsPageMenuItem.Label = AppLanguages.CurrentLanguage.MainMenuSettingsLabel;
                this.ShowHelpPageMenuItem.Label = AppLanguages.CurrentLanguage.MainMenuHelpLabel;
            }
        }

        public override void Appering()
        {
            base.Appering();

            if (String.IsNullOrEmpty(this.UserName) ||
                (String.IsNullOrEmpty(this.CompanyName)))
                this.UpdateUserData();
        }

        private void TripChanged(TripChangedMessage message)
        {
            _trip = ((message != null) && (message.Trip != null) ? message.Trip : null);
            var enabled = (_trip != null);

            this.ShowTripPageMenuItem.IsEnabled = enabled;
            this.ShowAdvancesPageMenuItem.IsEnabled = enabled;
            this.ShowDelayEmergencyPageMenuItem.IsEnabled = enabled;
            this.ShowRoutePageMenuItem.IsEnabled = enabled;
        }

        private void UpdateUserData()
        {
            Task.Run(async ()=>{
                try
                {
                    var user = await App.ServerManager.GetCurrentUser();
                    var company = await App.ServerManager.GetUserCompany();

                    var userName = String.Format("{0} {1}", user.FirstName, user.LastName);
                    var companyName = company.DisplayName;

                    this.UserName = userName;
                    this.CompanyName = companyName;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    ShowToastMessage.Send(exception.Message);
                }
            });
        }

        private void SelectItem(object parameter)
        {
            this.SelectedItem = null;

            MenuItem menuItem = (parameter as MenuItem);
            if ((menuItem != null) && (menuItem.Command.CanExecute(parameter)))
            {
                HideMainMenuMessage.Send();
                menuItem.Command.Execute(menuItem);
            }
        }

        private void ShowHomePage(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        private void ShowTripPage(object parameter)
        {
            if (_trip != null)
                ShowTripPageMessage.Send(_trip);
        }

        private void ShowAdvancesPage(object parameter)
        {
            if (_trip != null)
                ShowAdvancesPageMessage.Send(_trip);
        }

        private void ShowDelayEmergencyPage(object parameter)
        {
            if (_trip != null)
				ShowDelayEmergencyPageMessage.Send(_trip);
        }

        private void ShowRoutePage(object parameter)
        {
            if (_trip != null)
                ShowRoutePageMessage.Send(_trip);
        }

        private void ShowPointsAndRewardsPage(object parameter)
        {
            ShowPointsAndRewardsPageMessage.Send();
        }

        private void ShowSettingsPage(object parameter)
        {
            ShowSettingsPageMessage.Send();
        }

        private void ShowHelpPage(object parameter)
        {
            ShowHelpPageMessage.Send();
        }

        public MenuItem SelectedItem
        {
            get { return (this.GetValue("SelectedItem") as MenuItem); }
            set { this.SetValue("SelectedItem", value); }
        }

        public string UserName
        {
            get { return (string)this.GetValue("UserName"); }
            set { this.SetValue("UserName", value); }
        }

        public string CompanyName
        {
            get { return (string)this.GetValue("CompanyName"); }
            set { this.SetValue("CompanyName", value); }
        }

        public VisualCommand SelectItemCommand { get; private set; }

        public ObservableCollection<MenuItem> Items { get; private set; }

        public MenuItem ShowHomePageMenuItem { get; private set; }

        public MenuItem ShowTripPageMenuItem { get; private set; }

        public MenuItem ShowAdvancesPageMenuItem { get; private set; }

        public MenuItem ShowDelayEmergencyPageMenuItem { get; private set; }

        public MenuItem ShowRoutePageMenuItem { get; private set; }

        public MenuItem ShowPointsAndRewardsPageMenuItem { get; private set; }

        public MenuItem ShowSettingsPageMenuItem { get; private set; }

        public MenuItem ShowHelpPageMenuItem { get; private set; }
    }
    #endregion
}
