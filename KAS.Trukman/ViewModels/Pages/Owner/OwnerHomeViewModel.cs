using KAS.Trukman.Classes;
using KAS.Trukman.AppContext;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using KAS.Trukman.Data.Classes;

namespace KAS.Trukman.ViewModels.Pages.Owner
{
    #region OwnerHomeViewModel
    public class OwnerHomeViewModel : PageViewModel
    {
        private System.Timers.Timer _checkDriversTimer = null;

        public OwnerHomeViewModel()
            : base()
        {
            this.SelectItemCommand = new VisualCommand(this.SelectItem);
            this.ShowMainMenuCommand = new VisualCommand(this.ShowMainMenu);

            this.RateConfirmationCommandItem = new CommandItem(new VisualCommand(this.RateConfirmation));
            this.DispatchDriverCommandItem = new CommandItem(new VisualCommand(this.DispatchDriver));
            this.LoadConfirmationCommandItem = new CommandItem(new VisualCommand(this.LoadConfirmation));
            this.BrockerListCommandItem = new CommandItem(new VisualCommand(this.BrockerList));
            this.FuelAdvanceCommandItem = new CommandItem(new VisualCommand(this.FuelAdvance));
            this.TrackFleetCommandItem = new CommandItem(new VisualCommand(this.TrackFleet));
            this.LumperCommandItem = new CommandItem(new VisualCommand(this.Lumper));
            this.InvoiceCommandItem = new CommandItem(new VisualCommand(this.Invoice));
            this.ReportsCommandItem = new CommandItem(new VisualCommand(this.Reports));
            this.DelayAlertsCommandItem = new CommandItem(new VisualCommand(this.DelayAlerts));
            this.DeliveryUpdateCommandItem = new CommandItem(new VisualCommand(this.DeliveryUpdate));

            this.Items = new ObservableCollection<CommandItem>();
            this.Items.Add(this.RateConfirmationCommandItem);
            this.Items.Add(this.DispatchDriverCommandItem);
            this.Items.Add(this.LoadConfirmationCommandItem);
            this.Items.Add(this.BrockerListCommandItem);
            this.Items.Add(this.FuelAdvanceCommandItem);
            this.Items.Add(this.TrackFleetCommandItem);
            this.Items.Add(this.LumperCommandItem);
            this.Items.Add(this.InvoiceCommandItem);
            this.Items.Add(this.ReportsCommandItem);
            this.Items.Add(this.DelayAlertsCommandItem);
            this.Items.Add(this.DeliveryUpdateCommandItem);
        }

        public override void Appering()
        {
            base.Appering();

            this.Localize();

            this.CheckDrivers();
        }

        public override void Disappering()
        {
            this.StopCheckDriversTimer();

            base.Disappering();
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.AppName;

            if (this.RateConfirmationCommandItem != null)
            {
                this.RateConfirmationCommandItem.Label = AppLanguages.CurrentLanguage.OwnerHomeRateConfirmationCommandItemLabel;
                this.DispatchDriverCommandItem.Label = AppLanguages.CurrentLanguage.OwnerHomeDispatchDriverCommandItemLabel;
                this.LoadConfirmationCommandItem.Label = AppLanguages.CurrentLanguage.OwnerHomeLoadConfirmationCommandItemLabel;
                this.BrockerListCommandItem.Label = AppLanguages.CurrentLanguage.OwnerHomeBrockerListCommandItemLabel;
                this.FuelAdvanceCommandItem.Label = AppLanguages.CurrentLanguage.OwnerHomeFuelAdvanceCommandItemLabel;
                this.TrackFleetCommandItem.Label = AppLanguages.CurrentLanguage.OwnerHomeTrackFleetCommandItemLabel;
                this.LumperCommandItem.Label = AppLanguages.CurrentLanguage.OwnerHomeLumperCommandItemLabel;
                this.InvoiceCommandItem.Label = AppLanguages.CurrentLanguage.OwnerHomeInvoiceCommandItemLabel;
                this.ReportsCommandItem.Label = AppLanguages.CurrentLanguage.OwnerHomeReportsCommandItemLabel;
                this.DelayAlertsCommandItem.Label = AppLanguages.CurrentLanguage.OwnerHomeDelayAlertsCommandItem;
                this.DeliveryUpdateCommandItem.Label = AppLanguages.CurrentLanguage.OwnerHomeDeliveryUpdateCommandItemLabel;
            }
        }

        private void StartCheckDriversTimer()
        {
            if (_checkDriversTimer == null)
            {
                _checkDriversTimer = new System.Timers.Timer { Interval = 10000 };
                _checkDriversTimer.Elapsed += (sender, args) => {
                    this.CheckDrivers();
                };
            }
            _checkDriversTimer.Start();
        }

        private void StopCheckDriversTimer()
        {
            if (_checkDriversTimer != null)
                _checkDriversTimer.Stop();
        }

        private void CheckDrivers()
        {
            Task.Run(async () => {
                this.StopCheckDriversTimer();
                this.IsBusy = true;
                this.DisableCommands();
                try
                {
                    if (this.Company == null)
                        this.Company = await TrukmanContext.SelectUserCompany();
                    if (this.Company != null)
                    {
                        var user = await TrukmanContext.SelectRequestedUser(this.Company.ID); // App.ServerManager.GetRequestForCompany(this.Company.Name);
                        if (user != null)
                            ShowOwnerDriverAuthorizationPageMessage.Send(this.Company.Name, user, this.Company.ID);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    // To do: Show exception message
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    this.EnabledCommands();
                    this.IsBusy = false;
                    this.StartCheckDriversTimer();
                }
            });
        }
        private void SelectItem(object parameter)
        {
            this.SelectedItem = null;

            var commandItem = (parameter as CommandItem);
            if ((commandItem != null) && (commandItem.Command != null) && (commandItem.Command.CanExecute(parameter)))
                commandItem.Command.Execute(parameter);
        }

        private void ShowMainMenu(object parameter)
        {
            ShowMainMenuMessage.Send();
        }

        private void RateConfirmation(object parameter)
        {
        }

        private void DispatchDriver(object parameter)
        {
        }

        private void LoadConfirmation(object parameter)
        {
        }

        private void BrockerList(object parameter)
        {
            ShowOwnerBrockerListMessage.Send();
        }

        private void FuelAdvance(object parameter)
        {
            ShowOwnerFuelAdvancePageMessage.Send();
        }

        private void TrackFleet(object parameter)
        {
            ShowOwnerFleetPageMessage.Send();
        }

        private void Lumper(object parameter)
        {
            ShowOwnerLumperPageMessage.Send();
        }

        private void Invoice(object parameter)
        {
            ShowOwnerInvoiceListPageMessage.Send();
        }

        private void Reports(object parameter)
        {
        }

        private void DelayAlerts(object parameter)
        {
			ShowOwnerDelayAlertsPageMessage.Send ();
        }

        private void DeliveryUpdate(object parameter)
        {
			ShowOwnerDeliveryUpdatePageMessage.Send ();
        }

        public CommandItem SelectedItem
        {
            get { return (this.GetValue("SelectedItem") as CommandItem); }
            set { this.SetValue("SelectedItem", value); }
        }

        public Company Company
        {
            get { return (this.GetValue("Company") as Company); }
            set { this.SetValue("Company", value); }
        }

        public ObservableCollection<CommandItem> Items { get; private set; }

        public VisualCommand SelectItemCommand { get; private set; }

        public VisualCommand ShowMainMenuCommand { get; private set; }

        public CommandItem RateConfirmationCommandItem { get; private set; }

        public CommandItem DispatchDriverCommandItem { get; private set; }

        public CommandItem LoadConfirmationCommandItem { get; private set; }

        public CommandItem BrockerListCommandItem { get; private set; }

        public CommandItem FuelAdvanceCommandItem { get; private set; }

        public CommandItem TrackFleetCommandItem { get; private set; }

        public CommandItem LumperCommandItem { get; private set; }

        public CommandItem InvoiceCommandItem { get; private set; }

        public CommandItem ReportsCommandItem { get; private set; }

        public CommandItem DelayAlertsCommandItem { get; private set; }

        public CommandItem DeliveryUpdateCommandItem { get; private set; }
    }
    #endregion
}
