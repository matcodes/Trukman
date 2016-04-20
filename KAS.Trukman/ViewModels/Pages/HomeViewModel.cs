using KAS.Trukman.Classes;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Data.Interfaces;
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
using Trukman.Interfaces;
using System.Collections.ObjectModel;

namespace KAS.Trukman.ViewModels.Pages
{
    #region HomeViewModel
    public class HomeViewModel : PageViewModel
    {
        private System.Timers.Timer _waitForTripTimer = null;
        private System.Timers.Timer _tripProposedTimer = null;
        private System.Timers.Timer _currentTimeTimer = null;
        private System.Timers.Timer _checkGPSTimer = null;
		private System.Timers.Timer _driverOnPickupTimer = null;
		private System.Timers.Timer _driverOnDeliveryTimer = null;
		private System.Timers.Timer _tripCompletedTimer = null;

        private MenuItem _takePhotoFromCameraMenuItem = null;

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
			this.ShowCameraCommand = new VisualCommand (this.ShowCamera);
			this.RewardsCommand = new VisualCommand (this.ShowRewards);

            this.MenuItemClickCommand = new VisualCommand(this.MenuItemClick);
            this.TakePhotoFromCameraCommand = new VisualCommand(this.TakePhotoFromCamera);

            _takePhotoFromCameraMenuItem = new MenuItem(this.TakePhotoFromCameraCommand) {
               
            };

            this.ArrivedMenuItems = new ObservableCollection<MenuItem>();
            this.ArrivedMenuItems.Add(_takePhotoFromCameraMenuItem);
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);
        }

        public override void Appering()
        {
            base.Appering();

            this.StartWaitForTripTimer();
            this.StartCheckGPSTimer();
        }

        public override void Disappering()
        {
            this.StopCheckGPSTimer();
            this.StopWaitToTripTimer();

            base.Disappering();
        }

        public override bool HandleBackButton()
        {
            var result = base.HandleBackButton();
            if (this.State == HomeStates.TripDeclined)
            {
                result = true;
                this.State = HomeStates.TripPropesed;
            }
            return result;
        }

        protected override void DoPropertyChanged(string propertyName)
        {
            base.DoPropertyChanged(propertyName);

			if (propertyName == "Trip") {
				if ((this.Trip != null) && (this.Trip.Shipper != null))
					this.FindAddress (this.Trip.Shipper);

				this.TripOrigin = (this.Trip != null && this.Trip.Shipper != null ? this.Trip.Shipper.AddressLineFirst + " " + this.Trip.Shipper.AddressLineSecond : "");
				this.TripDestination = (this.Trip != null && this.Trip.Receiver != null ? this.Trip.Receiver.AddressLineFirst + " " + this.Trip.Receiver.AddressLineSecond : "");

				this.Localize ();
			} else if (propertyName == "State") {
				if (this.State == HomeStates.TripDeclined) {
					this.OtherReasonText = "";
					this.SelectedDeclinedReason = DeclinedReasonItems.Reason_1;
				} else if (this.State == HomeStates.WaitingForTrip) {
					this.StopTripCompletedTimer (); // Останавливаем таймер окончания работы от предыдущей итерации
					this.Trip = null;
					this.StartWaitForTripTimer ();
				} else if (this.State == HomeStates.TripCanceled) {
					this.StopTripProposedTimer ();
				} else if (this.State == HomeStates.TripPropesed) {
					this.StopWaitToTripTimer ();
					this.StartTripProposedTimer ();
				} else if (this.State == HomeStates.TripAccepted) {
					StartLocationServiceMessage.Send (this.Trip.TripId);

					this.StopTripProposedTimer ();


					TripChangedMessage.Send (this.Trip);
					this.SelectedContractor = ContractorItems.Origin;
					this.SetContractorAddress ();
					this.SetCurrentTime ();

					this.StartDriverOnPickupTimer ();
					this.StartCurrentTimeTimer ();
				} else if (this.State == HomeStates.ArrivedAtPickupOnTime) {
					this.StopCurrentTimeTimer ();
					this.StopDriverOnPickupTimer ();
					//TripChangedMessage.Send (null);
					this.SetCurrentTime ();
					this.SetArrivedAddress ();
					this.TotalPoints = this.Trip.Points;
					this.StartDriverOnDeliveryTimer ();
				} else if (this.State == HomeStates.ArrivedAtPickupLate) {
					this.StopCurrentTimeTimer ();
					this.StopDriverOnPickupTimer ();
					//TripChangedMessage.Send (null);
					this.SetCurrentTime ();
					this.SetArrivedAddress ();
					this.TotalPoints = this.Trip.Points;
					this.StartDriverOnDeliveryTimer ();
				} else if (this.State == HomeStates.ArrivedAtDestinationOnTime) {
					this.StopDriverOnDeliveryTimer ();
					this.SetArrivedAddress ();
				} else if (this.State == HomeStates.ArrivedAtDestinationLate) {
					this.StopDriverOnDeliveryTimer ();
					this.SetArrivedAddress ();
				} else if (this.State == HomeStates.TripComleted) {
					this.StartTripCompletedTimer ();
					//this.StopTripCompletedTimer ();
				}
			} else if (propertyName == "SelectedContractor") {
				this.SetContractorAddress ();
			} else if (propertyName == "TotalPoints") {
				this.Localize ();
			} else if (propertyName == "CurrentPosition") {
				if (this.State == HomeStates.TripAccepted || this.State == HomeStates.ArrivedAtPickupLate || this.State == HomeStates.ArrivedAtPickupOnTime)
					CheckArrived ();
			}
        }

        private void SetContractorAddress()
        {
            IContractor contractor = null;
            if (this.Trip != null)
                contractor = (this.SelectedContractor == ContractorItems.Origin ? (IContractor)this.Trip.Shipper : (IContractor)this.Trip.Receiver);
            if (contractor != null)
                this.FindAddress(contractor);
        }

        private void SetArrivedAddress()
        {
            IContractor contractor = null;
			if (this.Trip != null) {
				// Адрес грузоотправителя или грузополучателя
				if (this.State == HomeStates.TripAccepted)
					contractor = this.Trip.Shipper;
				else if (this.State == HomeStates.ArrivedAtPickupLate || this.State == HomeStates.ArrivedAtPickupOnTime)
					contractor = this.Trip.Receiver;
			}
            if (contractor != null)
                this.FindAddress(contractor);
        }

        private void StartWaitForTripTimer()
        {
            if (_waitForTripTimer == null)
            {
                _waitForTripTimer = new System.Timers.Timer { Interval = 10000 };
                _waitForTripTimer.Elapsed += (sender, args) =>
                {
					//this.StopWaitToTripTimer();
					this.CheckNewTrip();
                };
            }
			_waitForTripTimer.Start ();
        }

        private void StopWaitToTripTimer()
        {
            if (_waitForTripTimer != null)
                _waitForTripTimer.Stop();
        }

        private void StartTripProposedTimer()
        {
            if (_tripProposedTimer == null)
            {
                _tripProposedTimer = new System.Timers.Timer { Interval = 10000 };
                _tripProposedTimer.Elapsed += (sender, args) => 
                {
                    this.CheckTripCanceled();
                };
            }
            _tripProposedTimer.Start();
        }

        private void StopTripProposedTimer()
        {
            if (_tripProposedTimer != null)
                _tripProposedTimer.Stop();
        }

        private void StartCurrentTimeTimer()
        {
            if (_currentTimeTimer == null)
            {
                _currentTimeTimer = new System.Timers.Timer { Interval = 1000 };
                _currentTimeTimer.Elapsed += (sender, args) => 
                {
                    this.SetCurrentTime();
                };
            }
            _currentTimeTimer.Start();
        }

        private void SetCurrentTime()
        {
            var now = DateTime.Now;
			DateTime arrivedTime = DateTime.MinValue;
			if (this.State == HomeStates.TripAccepted)
				arrivedTime = this.Trip.PickupDatetime;
			else if (this.State == HomeStates.ArrivedAtPickupOnTime || this.State == HomeStates.ArrivedAtPickupLate)
				arrivedTime = this.Trip.DeliveryDatetime;

			this.IsTimeOver = (now > arrivedTime);
			var time = (this.IsTimeOver ? now - arrivedTime : arrivedTime - now);
			int hours = time.Days * 24 + time.Hours;
			this.CurrentTime = String.Format ("{0}:{1}:{2}", hours.ToString ().PadLeft (2, '0'), time.Minutes.ToString ().PadLeft (2, '0'), time.Seconds.ToString ().PadLeft (2, '0'));
        }

        private void StopCurrentTimeTimer()
        {
            if (_currentTimeTimer != null)
                _currentTimeTimer.Stop();
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

		private void StartDriverOnPickupTimer()
		{
			/*if (_driverOnPickupTimer == null) 
			{
				_driverOnPickupTimer = new System.Timers.Timer (10000);
				_driverOnPickupTimer.Elapsed += (object sender, ElapsedEventArgs e) => 
				{
					if (this.State != HomeStates.ArrivedAtPickupLate || this.State != HomeStates.ArrivedAtPickupOnTime) 
						this.CheckArrived();
				};
			}
			_driverOnPickupTimer.Start ();*/
		}

		private void StartDriverOnDeliveryTimer()
		{
			/*if (_driverOnDeliveryTimer == null) 
			{
				_driverOnDeliveryTimer = new System.Timers.Timer (10000);
				_driverOnDeliveryTimer.Elapsed += (object sender, ElapsedEventArgs e) => 
				{
					if (this.State != HomeStates.ArrivedAtDestinationOnTime || this.State != HomeStates.ArrivedAtDestinationLate) 
						this.CheckArrived();
				};
			}
			_driverOnDeliveryTimer.Start ();*/
		}

		private void StartTripCompletedTimer ()
		{
			if (_tripCompletedTimer == null) {
				_tripCompletedTimer = new System.Timers.Timer (10000);
				_tripCompletedTimer.Elapsed += (object sender, ElapsedEventArgs e) => 
				{
					this.CheckComplitedTrip();					
				};
			}
			_tripCompletedTimer.Start ();
		}

		private async void CheckComplitedTrip()
		{
			bool isCompleted = await App.ServerManager.IsCompletedTrip (this.Trip.TripId);
			if (isCompleted) {
				StopTripCompletedTimer ();
				this.State = HomeStates.WaitingForTrip;
			}
		}

		private void StopTripCompletedTimer()
		{
			if (_tripCompletedTimer != null)
				_tripCompletedTimer.Stop ();
		}

		private void StopDriverOnDeliveryTimer()
		{
			if (_driverOnDeliveryTimer != null)
				_driverOnDeliveryTimer.Stop ();
		}

		private void CheckArrived ()
		{
			Position currentPosition = App.LocManager.GetCurrentLocation ();
			// Считаем расстояние в милях
			Position destPosition = this.AddressPosition;
			if (this.State == HomeStates.ArrivedAtPickupLate || this.State == HomeStates.ArrivedAtPickupOnTime)
				destPosition = this.ArrivedPosition;
			
			double distanceInMiles = currentPosition.DistanceFrom (destPosition) * 0.00062136994937697;
			// TODO: дистанция меньше 1 мили
			if (distanceInMiles <= 1) 
			{
				this.Arrived (null);
			}
		}

		private void StopDriverOnPickupTimer()
		{
			if (_driverOnPickupTimer != null)
				_driverOnPickupTimer.Stop ();
		}

		private async void CheckNewTrip()
		{
			this.IsBusy = true;
			this.DisableCommands ();
			try {
				var tripId = SettingsServiceHelper.GetTripId ();

				// Ищем новую/текущую работу
				this.Trip = await App.ServerManager.GetNewOrCurrentTrip ();

				// На всякий случай проверяем state, чтобы повторно не выполнить код
				if (this.Trip != null && this.State == HomeStates.WaitingForTrip) 
				{
					if (this.Trip.DriverAccepted.GetValueOrDefault())
						this.State = HomeStates.TripAccepted;
					else
					{
						// Работа отменена диспетчером или владельцем
						if (this.Trip.JobCancelled)
							this.State = HomeStates.TripCanceled;
						else 
							this.State = HomeStates.TripPropesed;
					}
					SettingsServiceHelper.SaveTripId (this.Trip.TripId);
				}
			} 
			catch (Exception exception) {
				// To do: Show error message
				Console.WriteLine (exception);
			} 
			finally {
				this.EnabledCommands ();
				this.IsBusy = false;
			}
		}

        private async void CheckTripCanceled()
        {
            this.IsBusy = true;
            this.DisableCommands();
            try
            {
                // Check trip canceled
				// Thread.Sleep(1000);
				// Проверяем this.Trip, чтобы повторно не выполнить код из-за таймера
				if (this.Trip != null)
				{
					var trip = await App.ServerManager.GetNewOrCurrentTrip(this.Trip.TripId);
					if (trip != null && trip.JobCancelled)
					{
						this.State = HomeStates.TripCanceled;
					}
				}

            }
            catch (Exception exception)
            {
                // To do: Show exception message
                Console.WriteLine(exception);
            }
            finally
            {
                this.EnabledCommands();
                this.IsBusy = false;
            }
        }

        private async void DeclineTrip()
        {
			this.IsBusy = true;
			this.DisableCommands ();
			try {
				string reason = this.SelectedDeclinedReason.ToString();
				if (!string.IsNullOrEmpty(this.OtherReasonText))
					reason = string.Format("{0}: {1}", reason, this.OtherReasonText);

				await App.ServerManager.DeclineTrip(this.Trip.TripId, reason);
				//Thread.Sleep (1000);
				this.State = HomeStates.WaitingForTrip;
			} catch (Exception exception) {
				// To do: Show error message
				Console.WriteLine (exception);
			} finally {
				this.EnabledCommands ();
				this.IsBusy = false;
			}
		}

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.AppName;
            this.TripTime = (this.Trip != null ? AppLanguages.GetTimeString(this.Trip.PickupDatetime) : "");
            this.TripPoints = (this.Trip != null ? String.Format(AppLanguages.CurrentLanguage.HomePointsLabel, this.Trip.Points) : "");
            this.TotalPointsText = String.Format(AppLanguages.CurrentLanguage.HomeTotalPointsLabel, this.TotalPoints);
        }

        private void FindAddress(IContractor contractor)
        {
            if (contractor != null)
            {
				Task.Run (async () => {
					var geocoder = new Geocoder ();
					var locations = await geocoder.GetPositionsForAddressAsync (contractor.AddressLineFirst + " " + contractor.AddressLineSecond);

					var location = locations.FirstOrDefault ();
					if ((location.Latitude == 0) && (location.Longitude == 0) && !string.IsNullOrEmpty (contractor.AddressLineSecond)) {
						locations = await geocoder.GetPositionsForAddressAsync (contractor.AddressLineSecond);
						location = locations.FirstOrDefault ();
					}

					if (this.State == HomeStates.TripPropesed)
						this.AddressPosition = location;
					else if ((this.State == HomeStates.ArrivedAtPickupLate) || (this.State == HomeStates.ArrivedAtPickupOnTime))
						this.ArrivedPosition = location;
					else
						this.ContractorPosition = location;
				});
            }
        }

        private void ShowMainMenu(object parameter)
        {
            if (this.State == HomeStates.TripDeclined)
                this.State = HomeStates.TripPropesed;
            else
                ShowMainMenuMessage.Send();
        }

        private void Decline(object parameter)
        {
            this.StopTripProposedTimer();
            this.State = HomeStates.TripDeclined; 
        }

		private void AcceptTrip ()
		{
			App.ServerManager.AcceptTrip (this.Trip.TripId);
			//StartLocationServiceMessage.Send (this.Trip.TripId);
		}

        private void Accept(object parameter)
        {
            this.StopTripProposedTimer();
			this.AcceptTrip ();
            this.State = HomeStates.TripAccepted;
        }

        private void DeclinedSubmit(object parameter)
        {
			if ((this.SelectedDeclinedReason == DeclinedReasonItems.Other) && (String.IsNullOrEmpty (this.OtherReasonText)))
				ShowToastMessage.Send (AppLanguages.CurrentLanguage.HomeDeclinedSubmitErrorText);
			else {
				this.DeclineTrip ();
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
            this.State = HomeStates.WaitingForTrip;
        }

        private void Arrived(object parameter)
        {
			// Водитель добрался до пункта загрузки
			if (this.State == HomeStates.TripAccepted) {
				this.State = (this.IsTimeOver ? HomeStates.ArrivedAtPickupLate : HomeStates.ArrivedAtPickupOnTime);
				App.ServerManager.SetDriverPickupOnTime (this.Trip.TripId, this.State == HomeStates.ArrivedAtPickupOnTime);
			} 
			//Водитель добрался до пункта разгрузки
			else if (this.State == HomeStates.ArrivedAtPickupOnTime || this.State == HomeStates.ArrivedAtPickupLate) {
				this.State = (this.IsTimeOver ? HomeStates.ArrivedAtDestinationLate : HomeStates.ArrivedAtDestinationOnTime);
				App.ServerManager.SetDriverDestinationOnTime (this.Trip.TripId, this.State == HomeStates.ArrivedAtDestinationOnTime);

				// TODO: запускаем таймер на 10 сек. и после ставим статус TripCompleted
				var task = new Task (() => {
					Thread.Sleep (10000);
					this.State = HomeStates.TripComleted;
				});
				task.Start();
			}
		}

		private void ArrivedDestination(object parameter)
		{
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

		private void ShowCamera(object parameter)
		{
			ShowCameraMessage.Send ();
		}

		private void ShowRewards(object parameter)
		{
		}

        private void MenuItemClick(object parameter)
        {
            var menuItem = (parameter as MenuItem);
            if ((menuItem != null) && (menuItem.Command != null) && (menuItem.Command.CanExecute(parameter)))
                menuItem.Command.Execute(parameter);
        }

        private void TakePhotoFromCamera(object parameter)
        {
            TakePhotoFromCameraMessage.Send(this.Trip);
        }

        public HomeStates State
        {
            get { return (HomeStates)this.GetValue("State", HomeStates.WaitingForTrip); }
            set { this.SetValue("State", value); }
        }

        public ITrip Trip
        {
            get { return (this.GetValue("Trip") as ITrip); }
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

        public int TotalPoints
        {
            get { return (int)this.GetValue("TotalPoints", (int)0); }
            set { this.SetValue("TotalPoints", value); }
        }

        public string TotalPointsText
        {
            get { return (string)this.GetValue("TotalPointsText"); }
            set { this.SetValue("TotalPointsText", value); }
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

        public ObservableCollection<MenuItem> ArrivedMenuItems { get; private set; }

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

		public VisualCommand ShowCameraCommand { get; private set; }

		public VisualCommand RewardsCommand { get; private set; }

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
