using KAS.Trukman.Classes;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using KAS.Trukman.ViewModels.Pages;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms.Maps;
using KAS.Trukman.Helpers;
using Trukman.Helpers;
using Trukman.Messages;
using System.Collections.ObjectModel;
using KAS.Trukman.AppContext;
using Xamarin.Forms;
using KAS.Trukman.Data.Enums;

namespace KAS.Trukman.ViewModels.Pages
{
    #region HomeViewModel
    public class HomeViewModel : PageViewModel
    {
        private CommandItem _pickUpTakePhotoFromCameraMenuItem = null;
        private CommandItem _deliveryTakePhotoFromCameraMenuItem = null;

        private System.Timers.Timer _checkGPSTimer = null;
        private System.Timers.Timer _arrivedTimeTimer = null;

        public HomeViewModel() 
            : base()
        {
            this.ShowMainMenuCommand = new VisualCommand(this.ShowMainMenu);
            this.DeclineCommand = new VisualCommand(this.Decline);
            this.AcceptCommand = new VisualCommand(this.Accept);
            this.DeclinedSubmitCommand = new VisualCommand(this.DeclinedSubmit);
            this.SelectDeclinedReasonCommand = new VisualCommand(this.SelectDeclinedReason);
            this.ContinueCommand = new VisualCommand(this.Continue);
            this.SelectContractorCommand = new VisualCommand(this.SelectContractor);
            //this.ArrivedCommand = new VisualCommand(this.Arrived);
            this.ShowGPSPreferencesCommand = new VisualCommand(this.ShowGPSPreferences);
            this.GPSPopupSettingsCommand = new VisualCommand(this.GPSPopupSettings);
            this.GPSPopupCancelCommand = new VisualCommand(this.GPSPopupCancel);
			this.RewardsCommand = new VisualCommand (this.Rewards);
			this.NewTripCommand = new VisualCommand (this.NewTrip);

            this.MenuItemClickCommand = new VisualCommand(this.MenuItemClick);
            this.TakePhotoFromCameraCommand = new VisualCommand(this.TakePhotoFromCamera);

            _pickUpTakePhotoFromCameraMenuItem = new CommandItem(this.TakePhotoFromCameraCommand) {
                Icon = PlatformHelper.CameraImageSource               
            };

            _deliveryTakePhotoFromCameraMenuItem = new CommandItem(this.TakePhotoFromCameraCommand) {
                Icon = PlatformHelper.CameraImageSource
            };

            this.ArrivedPickUpMenuItems = new ObservableCollection<CommandItem>();
            this.ArrivedPickUpMenuItems.Add(_pickUpTakePhotoFromCameraMenuItem);

            this.ArrivedDeliveryMenuItems = new ObservableCollection<CommandItem>();
            this.ArrivedDeliveryMenuItems.Add(_deliveryTakePhotoFromCameraMenuItem);

            this.Localize();
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);
        }

        public override void Appering()
        {
            base.Appering();

            this.UpdateState();

            DriverLocationChangedMessage.Subscribe(this, this.DriverLocationChanged);
            DriverTripContextChangedMessage.Subscribe(this, this.DriverTripContextChanged);
            TripShipperPositionChangedMessage.Subscribe(this, this.TripShipperPositionChanged);
            TripReceiverPositionChangedMessage.Subscribe(this, this.TripReceiverPositionChanged);
			StopBusyMessage.Subscribe (this, this.StopBusy);

            this.StartCheckGPSTimer();
            this.SetCurrentTime();
            this.StartArrivedTimeTimer();
        }

        public override void Disappering()
        {
            this.StopCheckGPSTimer();
            this.StopArrivedTimeTimer();

			StopBusyMessage.Unsubscribe (this);
            DriverLocationChangedMessage.Unsubscribe(this);
            DriverTripContextChangedMessage.Unsubscribe(this);
            TripShipperPositionChangedMessage.Unsubscribe(this);
            TripReceiverPositionChangedMessage.Unsubscribe(this);

            base.Disappering();
        }

        public override bool HandleBackButton()
        {
            var result = base.HandleBackButton();
            if (this.State == HomeStates.TripDeclined)
            {
                this.DeclineBack();
                result = true;
            }
            return result;
        }

        protected override void DoPropertyChanged(string propertyName)
        {
            base.DoPropertyChanged(propertyName);

            if (propertyName == "Trip")
            {
                this.TripOrigin = (this.Trip != null && this.Trip.Shipper != null ? this.Trip.Shipper.Address : "");
                this.TripDestination = (this.Trip != null && this.Trip.Receiver != null ? this.Trip.Receiver.Address : "");
                this.Localize();
            }
            else if (propertyName == "State")
            {
                if (this.State == HomeStates.TripDeclined)
                {
                    this.OtherReasonText = "";
                    this.SelectedDeclinedReason = DeclinedReasonItems.Reason_1;
                }
                else if (this.State == HomeStates.WaitingForTrip)
                {
                }
                else if (this.State == HomeStates.TripCanceled)
                {
                }
                else if (this.State == HomeStates.TripPropesed)
                {
                    this.SetAddressPosition();
                }
                else if (this.State == HomeStates.TripAccepted)
                {
                    this.SelectedContractor = (TrukmanContext.Driver.Trip.IsPickup ? ContractorItems.Destination : ContractorItems.Origin);
					this.SetContractorAddress ();
                }
                else if (this.State == HomeStates.ArrivedAtPickupOnTime)
                {
                }
                else if (this.State == HomeStates.ArrivedAtPickupLate)
                {
                }
                else if (this.State == HomeStates.ArrivedAtDestinationOnTime)
                {
                }
                else if (this.State == HomeStates.ArrivedAtDestinationLate)
                {
                }
                else if (this.State == HomeStates.TripComleted)
                {
                }
				this.GetJobPoints ();
            }
            else if (propertyName == "SelectedContractor")
            {
                this.SetContractorAddress();
            }
			else if ((propertyName == "TotalJobPoints") || (propertyName == "TotalDriverPoints"))
            {
                this.Localize();
            }
            else if ((propertyName == "SelectedArrivedMenuItem") && (this.SelectedArrivedMenuItem != null))
                this.SelectedArrivedMenuItem = null;
        }

		private void GetJobPoints()
		{
			Task.Run (async() => {
				this.TotalJobPoints = await TrukmanContext.GetPointsByJobIDAsync(TrukmanContext.Driver.TripID);
				this.TotalDriverPoints = await TrukmanContext.GetPointsByDriverIDAsync(TrukmanContext.User.ID);
			});
		}

        private void SetContractorAddress()
        {
            var position = (this.SelectedContractor == ContractorItems.Origin ? TrukmanContext.Driver.ShipperPosition : TrukmanContext.Driver.ReceiverPosition);
            if ((position.Latitude == 0) && (position.Longitude == 0))
                position = TrukmanContext.Driver.Location;
			if ((this.ContractorPosition.Latitude != position.Latitude) || (this.ContractorPosition.Longitude != position.Longitude))
				this.ContractorPosition = position;
        }

        private void SetAddressPosition()
        {
            var position = TrukmanContext.Driver.ShipperPosition;
            if ((position.Latitude == 0) && (position.Longitude == 0))
                position = TrukmanContext.Driver.Location;
			if ((this.AddressPosition.Latitude != position.Latitude) || (this.AddressPosition.Longitude != position.Longitude))
				this.AddressPosition = position;
        }

        private void SetCurrentTime()
        {
            this.StopArrivedTimeTimer();
            try
            {
                var currentTime = "";
                if (this.Trip != null)
                {
                    var now = DateTime.Now;
                    DateTime arrivedTime = DateTime.MinValue;
                    if ((this.Trip.DriverAccepted) && (!this.Trip.IsPickup))
                        arrivedTime = this.Trip.PickupDatetime;
                    else if ((this.Trip.DriverAccepted) && (this.Trip.IsPickup) && (!this.Trip.IsDelivery))
                        arrivedTime = this.Trip.DeliveryDatetime;

                    if (arrivedTime != DateTime.MinValue)
                    {
                        this.IsTimeOver = (now > arrivedTime);
                        var time = (this.IsTimeOver ? now - arrivedTime : arrivedTime - now);
                        int hours = time.Days * 24 + time.Hours;
                        currentTime = String.Format("{0}:{1}:{2}", hours.ToString().PadLeft(2, '0'), time.Minutes.ToString().PadLeft(2, '0'), time.Seconds.ToString().PadLeft(2, '0'));
                    }
                }
                this.CurrentTime = currentTime;
            }
            finally
            {
                this.StartArrivedTimeTimer();
            }
        }

        private void StartCheckGPSTimer()
        {
            if (_checkGPSTimer == null)
            {
                _checkGPSTimer = new System.Timers.Timer { Interval = 1000 };
                _checkGPSTimer.Elapsed += (sender, args) =>
                {
                    _checkGPSTimer.Stop();
                    try
                    {
                        var enabled = PlatformHelper.CheckGPS();
                        if (enabled)
                            this.GPSState = GPSStates.On;
                        else
                        {
                            if ((this.State == HomeStates.TripAccepted) ||
                                (this.State == HomeStates.ArrivedAtPickupLate) ||
                                (this.State == HomeStates.ArrivedAtPickupOnTime))
                                this.GPSState = GPSStates.OffWarning;
                            else
                                this.GPSState = GPSStates.Off;
                        }
                    }
                    finally
                    {
                        _checkGPSTimer.Start();
                    }
                };
            }
            _checkGPSTimer.Start();
        }

        private void StopCheckGPSTimer()
        {
            if (_checkGPSTimer != null)
                _checkGPSTimer.Stop();
        }

        private void StartArrivedTimeTimer()
        {
            if (_arrivedTimeTimer == null)
            {
                _arrivedTimeTimer = new System.Timers.Timer { Interval = 1000 };
                _arrivedTimeTimer.Elapsed += (sender, args) => 
                {
                    this.SetCurrentTime();
                };
            }
            _arrivedTimeTimer.Start();
        }

        private void StopArrivedTimeTimer()
        {
            if (_arrivedTimeTimer != null)
                _arrivedTimeTimer.Stop();
        }

        protected override void Localize()
        {
            base.Localize();

			Device.BeginInvokeOnMainThread (() => {
				this.Title = AppLanguages.CurrentLanguage.AppName;
				this.TripTime = (this.Trip != null ? AppLanguages.GetTimeString ((this.Trip.IsPickup ? this.Trip.DeliveryDatetime : this.Trip.PickupDatetime)) : "");
				this.TripPoints = (this.Trip != null ? String.Format (AppLanguages.CurrentLanguage.HomePointsLabel, this.Trip.Points) : "");
				this.ArrivedTotalPointsText = String.Format(AppLanguages.CurrentLanguage.HomeArrivedTotalPointsLabel, this.TotalJobPoints);
				this.TotalJobPointsText = String.Format (AppLanguages.CurrentLanguage.HomeJobTotalPointsLabel, this.TotalJobPoints);
				this.TotalDriverPointsText = String.Format(AppLanguages.CurrentLanguage.HomeDriverTotalPointsLabel, this.TotalDriverPoints);

				if (_pickUpTakePhotoFromCameraMenuItem != null) {
					_pickUpTakePhotoFromCameraMenuItem.Label = AppLanguages.CurrentLanguage.HomeBonusPointsForPickupPhotoLabel;
					_pickUpTakePhotoFromCameraMenuItem.Description = AppLanguages.CurrentLanguage.HomeBonusPointsForTimeLabel;
				}

				if (_deliveryTakePhotoFromCameraMenuItem != null) {
					_deliveryTakePhotoFromCameraMenuItem.Label = AppLanguages.CurrentLanguage.HomeBonusPointsForDeliveryPhotoLabel;
					_deliveryTakePhotoFromCameraMenuItem.Description = AppLanguages.CurrentLanguage.HomeBonusPointsForTimeLabel;
				}
			});
        }

        private void DriverLocationChanged(DriverLocationChangedMessage message)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => 
            {
                Console.WriteLine("Driver location changed.");
            });
        }

        private void DriverTripContextChanged(DriverTripContextChangedMessage message)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => 
            {
                try
                {
                    this.UpdateState();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    // To do: Show exception message
                }
                finally
                {
                    this.IsBusy = false;
                }
            });
        }

        private void TripShipperPositionChanged(TripShipperPositionChangedMessage message)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => 
            {
                this.SetAddressPosition();
                this.SetContractorAddress();
            });
        }

        private void TripReceiverPositionChanged(TripReceiverPositionChangedMessage message)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                this.SetContractorAddress();
            });
        }

		private void StopBusy(StopBusyMessage message)
		{
			Xamarin.Forms.Device.BeginInvokeOnMainThread (() => {
				this.IsBusy = false;
			});
		}

        private void UpdateState()
        {
            try
            {
                this.State = (HomeStates)TrukmanContext.Driver.TripState;
                this.Trip = TrukmanContext.Driver.Trip;
                this.SetCurrentTime();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void ShowMainMenu(object parameter)
        {
            if (this.State == HomeStates.TripDeclined)
                this.DeclineBack();
            else
                ShowMainMenuMessage.Send();
        }

        private void Decline(object parameter)
        {
            this.IsBusy = true;
            DeclineTripChangedMessage.Send();
        }

        private void DeclineBack()
        {
            this.IsBusy = true;
            DeclineTripChangedBackMessage.Send();
        }

        private void Accept(object parameter)
        {
            this.IsBusy = true;
            AcceptTripChangedMessage.Send();
        }

        private void DeclinedSubmit(object parameter)
        {
			if ((this.SelectedDeclinedReason == DeclinedReasonItems.Other) && (String.IsNullOrEmpty (this.OtherReasonText)))
				ShowToastMessage.Send (AppLanguages.CurrentLanguage.HomeDeclinedSubmitErrorText);
			else
            {
                this.IsBusy = true;
                TaskRequestDeclineReasons declineReason = TaskRequestDeclineReasons.None;
                var reasonText = "";
                if (this.SelectedDeclinedReason == DeclinedReasonItems.Reason_1)
                {
                    declineReason = TaskRequestDeclineReasons.Reason1;
                    //reasonText = AppLanguages.CurrentLanguage.HomeDeclinedReason_1;
                }
                else if (this.SelectedDeclinedReason == DeclinedReasonItems.Reason_2)
                {
                    declineReason = TaskRequestDeclineReasons.Reason2;
                    //reasonText = AppLanguages.CurrentLanguage.HomeDeclinedReason_2;
                }
                else if (this.SelectedDeclinedReason == DeclinedReasonItems.Other)
                {
                    declineReason = TaskRequestDeclineReasons.Other;
                    //reasonText = this.OtherReasonText;
                }
                reasonText = this.OtherReasonText;
                this.IsBusy = true;
                DeclineTripSubmitMessage.Send((int)declineReason, reasonText);
			}
        }

        private void SelectDeclinedReason(object parameter)
        {
            if (parameter != null)
                this.SelectedDeclinedReason = (DeclinedReasonItems)parameter;
        }

        private void SelectContractor(object parameter)
        {
            if (parameter != null)
                this.SelectedContractor = (ContractorItems)parameter;
        }                                                                                   

        private void ShowGPSPreferences(object parameter)
        {
            if (this.GPSState != GPSStates.On)
                this.GPSPopupVisible = true;
        }

        private void Continue(object parameter)
        {
            CancelledTripChangedMessage.Send();
        }

        private void GPSPopupSettings(object parameter)
        {
            this.GPSPopupVisible = false;
            ShowGPSSettingsMessage.Send();
        }

        private void GPSPopupCancel(object parameter)
        {
            this.GPSPopupVisible = false;
        }

		private void Rewards(object parameter)
		{
            CompletedTripChangedMessage.Send();
            ShowPointsAndRewardsPageMessage.Send();
		}

		private void NewTrip(object parameter)
		{
			CompletedTripChangedMessage.Send ();
		}

        private void MenuItemClick(object parameter)
        {
            var menuItem = (parameter as KAS.Trukman.Classes.MenuItem);
            if ((menuItem != null) && (menuItem.Command != null) && (menuItem.Command.CanExecute(parameter)))
                menuItem.Command.Execute(parameter);
        }

        private void TakePhotoFromCamera(object parameter)
        {
            this.IsBusy = true;
            TakePhotoFromCameraMessage.Send();
        }

        public HomeStates State
        {
            get { return (HomeStates)this.GetValue("State", HomeStates.WaitingForTrip); }
            set { this.SetValue("State", value); }
        }

        public Trip Trip
        {
            get { return (this.GetValue("Trip") as Trip); }
            set { this.SetValue("Trip", value); }
        }

        public string TripOrigin
        {
            get { return (string)this.GetValue("TripOrigin"); }
            set { this.SetValue("TripOrigin", value); }
        }

        public string TripDestination
        {
            get { return (string)this.GetValue("TripDestination"); }
            set { this.SetValue("TripDestination", value); }
        }

        public string TripTime
        {
            get { return (string)this.GetValue("TripTime"); }
            set { this.SetValue("TripTime", value); }
        }

        public string TripPoints
        {
            get { return (string)this.GetValue("TripPoints"); }
            set { this.SetValue("TripPoints", value); }
        }

        public Position AddressPosition
        {
            get { return (Position)this.GetValue("AddressPosition", new Position()); }
            set { this.SetValue("AddressPosition", value); }
        }

        public Position ContractorPosition
        {
            get { return (Position)this.GetValue("ContractorPosition", new Position()); }
            set { this.SetValue("ContractorPosition", value); }
        }

        public DeclinedReasonItems SelectedDeclinedReason
        {
            get { return (DeclinedReasonItems)this.GetValue("SelectedDeclinedReason", DeclinedReasonItems.Reason_1); }
            set { this.SetValue("SelectedDeclinedReason", value); }
        }

        public string CurrentTime
        {
            get { return (string)this.GetValue("CurrentTime"); }
            set { this.SetValue("CurrentTime", value); }
        }

        public bool IsTimeOver
        {
            get { return (bool)this.GetValue("IsTimeOver", false); }
            set { this.SetValue("IsTimeOver", value); }
        }

        public bool ArrivedBonusMinsVisible
        {
            get { return (bool)this.GetValue("ArrivedBonusMinsVisible", true); }
            set { this.SetValue("ArrivedBonusMinsVisible", value); }
        }

        public int TotalJobPoints
        {
			get { return (int)this.GetValue("TotalJobPoints", (int)0); }
			set { this.SetValue("TotalJobPoints", value); }
        }

		public int TotalDriverPoints
		{
			get { return (int)this.GetValue ("TotalDriverPoints", (int)0); }
			set { this.SetValue ("TotalDriverPoints", value); }
		}

		public string ArrivedTotalPointsText
		{
			get { return (string)this.GetValue ("ArrivedTotalPointsText"); }
			set { this.SetValue ("ArrivedTotalPointsText", value); }
		}

        public string TotalJobPointsText
        {
			get { return (string)this.GetValue("TotalJobPointsText"); }
			set { this.SetValue("TotalJobPointsText", value); }
        }

		public string TotalDriverPointsText
		{
			get { return (string)this.GetValue ("TotalDriverPointsText"); }
			set { this.SetValue ("TotalDriverPointsText", value); }
		}

        public Position ArrivedPosition
        {
            get { return (Position)this.GetValue("ArrivedPosition", new Position()); }
            set { this.SetValue("ArrivedPosition", value); }
        }

        public string OtherReasonText
        {
            get { return (string)this.GetValue("OtherReasonText"); }
            set { this.SetValue("OtherReasonText", value); }
        }

        public GPSStates GPSState
        {
            get { return (GPSStates)this.GetValue("GPSState", GPSStates.Off); }
            set { this.SetValue("GPSState", value); }
        }

        public bool GPSPopupVisible
        {
            get { return (bool)this.GetValue("GPSPopupVisible", false); }
            set { this.SetValue("GPSPopupVisible", value); }
        }

        public ContractorItems SelectedContractor
        {
            get { return (ContractorItems)this.GetValue("SelectedContractor", ContractorItems.Origin); }
            set { this.SetValue("SelectedContractor", value); }
        }

		public Position CurrentPosition
		{
			get { return (Position)this.GetValue ("CurrentPosition"); }
			set { this.SetValue ("CurrentPosition", value);}
		}

        public KAS.Trukman.Classes.MenuItem SelectedArrivedMenuItem
        {
            get { return (this.GetValue("SelectedArrivedMenuItem") as KAS.Trukman.Classes.MenuItem); }
            set { this.SetValue("SelectedArrivedMenuItem", value); }
        }

        public ObservableCollection<CommandItem> ArrivedPickUpMenuItems { get; private set; }

        public ObservableCollection<CommandItem> ArrivedDeliveryMenuItems { get; private set; }

        public VisualCommand ShowMainMenuCommand { get; private set; }

        public VisualCommand DeclineCommand { get; private set; }

        public VisualCommand AcceptCommand { get; private set; }

        public VisualCommand DeclinedSubmitCommand { get; private set; }

        public VisualCommand SelectDeclinedReasonCommand { get; private set; }

        public VisualCommand ContinueCommand { get; private set; }

        public VisualCommand SelectContractorCommand { get; private set; }

        public VisualCommand ArrivedCommand { get; private set; }

        public VisualCommand ShowGPSPreferencesCommand { get; private set; }

        public VisualCommand GPSPopupSettingsCommand { get; private set; }

        public VisualCommand GPSPopupCancelCommand { get; private set; }

		public VisualCommand RewardsCommand { get; private set; }

		public VisualCommand NewTripCommand { get; private set; }

        public VisualCommand MenuItemClickCommand { get; private set; }

        public VisualCommand TakePhotoFromCameraCommand { get; private set; }
    }
    #endregion

    #region HomeStates
    public enum HomeStates
    {
        WaitingForTrip = 0,
        TripPropesed = 1,
        TripDeclined = 2,
        TripCanceled = 3,
        TripAccepted = 4,
        ArrivedAtPickupOnTime = 5,
        ArrivedAtPickupLate = 6,
		ArrivedAtDestinationOnTime = 7,
		ArrivedAtDestinationLate = 8,
		TripComleted = 9
    }
    #endregion

    #region DeclinedReasonItems
    public enum DeclinedReasonItems
    {
        Reason_1 = 0,
        Reason_2 = 1,
        Other = 2
    }
    #endregion

    #region ContractorItems
    public enum ContractorItems
    {
        Origin = 0,
        Destination = 1
    }
    #endregion

    #region GPSStates
    public enum GPSStates
    {
        On = 0,
        Off = 1,
        OffWarning = 2
    }
    #endregion
}
