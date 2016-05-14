using KAS.Trukman.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using KAS.Trukman.Data.Interfaces;
using Trukman.Helpers;
using KAS.Trukman.AppContext;

namespace KAS.Trukman.ViewModels.Pages
{
    #region AdvancesViewModel
    public class AdvancesViewModel : PageViewModel
    {
        private System.Timers.Timer _fuelRequestedTimer = null;
        private System.Timers.Timer _fuelReceivedTimer = null;
        private System.Timers.Timer _lumperRequestedTimer = null;
        private System.Timers.Timer _lumperReceivedTimer = null;

        public AdvancesViewModel() 
            : base()
        {
            this.ShowMainMenuCommand = new VisualCommand(this.ShowMainMenu);
            this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);
            this.FuelRequestCommand = new VisualCommand(this.FuelRequest);
            this.FuelResendCommand = new VisualCommand(this.FuelResend);
            this.FuelCancelCommand = new VisualCommand(this.FuelCancel);
            this.LumperRequestCommand = new VisualCommand(this.LumperRequest);
            this.LumperResendCommand = new VisualCommand(this.LumperResend);
            this.LumperCancelCommand = new VisualCommand(this.LumperCancel);
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);

			ITrip trip = (parameters != null && parameters.Length > 0 ? (parameters[0] as ITrip) : null);
			this.Trip = trip;

			// To do: get fuel comcheck
			this.GetFuelComcheck ();

            // To do: get lumper comcheck
			this.GetLumperComcheck ();
        }

        public override void Appering()
        {
            base.Appering();
        }

        public override void Disappering()
        {
            this.FuelStopReceivedTimer();
            this.FuelStopRequestedTimer();
            this.LumperStopReceivedTimer();
            this.LumperStopRequestedTimer();

            base.Disappering();
        }

        protected override void DoPropertyChanged(string propertyName)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (propertyName == "FuelState")
                {
                    this.FuelSetStateText();
                    this.FuelNoneButtonVisible = (this.FuelState == FuelAdvanceStates.None);
                    this.FuelRequestedButtonsVisible = (this.FuelState == FuelAdvanceStates.Requested);
                    this.FuelReceivedTextInfoVisible = (this.FuelState == FuelAdvanceStates.Received);
                }
                else if (propertyName == "LumperState")
                {
                    this.LumperSetStateText();
                    this.LumperNoneButtonVisible = (this.LumperState == LumperStates.None);
                    this.LumperRequestedButtonsVisible = (this.LumperState == LumperStates.Requested);
                    this.LumperReceivedTextInfoVisible = (this.LumperState == LumperStates.Received);
                }
            });

            base.DoPropertyChanged(propertyName);
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.AdvancesPageName;
            this.FuelSetStateText();
            this.LumperSetStateText();
        }

        protected override void DisableCommands()
        {
            base.DisableCommands();

            Device.BeginInvokeOnMainThread(() =>
            {
                this.ShowMainMenuCommand.IsEnabled = false;
                this.ShowHomePageCommand.IsEnabled = false;
                this.FuelRequestCommand.IsEnabled = false;
                this.FuelResendCommand.IsEnabled = false;
                this.FuelCancelCommand.IsEnabled = false;
                this.LumperRequestCommand.IsEnabled = false;
                this.LumperResendCommand.IsEnabled = false;
                this.LumperCancelCommand.IsEnabled = false;
            });
        }

        protected override void EnabledCommands()
        {
            base.EnabledCommands();

            Device.BeginInvokeOnMainThread(() =>
            {
                this.ShowMainMenuCommand.IsEnabled = true;
                this.ShowHomePageCommand.IsEnabled = true;
                this.FuelResendCommand.IsEnabled = true;
                this.FuelCancelCommand.IsEnabled = true;
                this.LumperResendCommand.IsEnabled = true;
                this.LumperCancelCommand.IsEnabled = true;
    			this.FuelRequestCommand.IsEnabled = (TrukmanContext.Driver.TripState == 4);
				this.LumperRequestCommand.IsEnabled = (TrukmanContext.Driver.TripState > 6);
            });
        }

		private void GetLumperComcheck ()
		{
			Task.Run (() => this.CheckLumperComcheck ());		
		}

		private void GetFuelComcheck()
		{
			Task.Run (() => this.CheckFuelComcheck ());
		}

        private void FuelSetStateText()
        {
            var stateText = "";
            var stateInfoText = "";
            if (this.FuelState == FuelAdvanceStates.None)
            {
                stateText = AppLanguages.CurrentLanguage.FuelAdvanceNoneLabel;
                stateInfoText = "";
            }
            else if (this.FuelState == FuelAdvanceStates.Requested)
            {
                stateText = AppLanguages.CurrentLanguage.FuelAdvanceRequestedLabel;
                stateInfoText = "";
            }
            else if (this.FuelState == FuelAdvanceStates.Received)
            {
                stateText = AppLanguages.CurrentLanguage.FuelAdvanceReceivedLabel;
                stateInfoText = AppLanguages.CurrentLanguage.FuelAdvanceReceivedInfoLabel;
            }
            else if (this.FuelState == FuelAdvanceStates.Completed)
            {
                stateText = String.Format(AppLanguages.CurrentLanguage.FuelAdvanceCompletedLabel, this.FuelComcheck);
                stateInfoText = "";
            }
            this.FuelStateText = stateText;
            this.FuelStateInfoText = stateInfoText;
        }

        private void ShowMainMenu(object parameter)
        {
            ShowMainMenuMessage.Send();
        }

        private void ShowHomePage(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        private void FuelRequest(object parameter)
        {
            Task.Run(async () => {
                this.DisableCommands();
                this.IsBusy = true;
                try
                {
					await TrukmanContext.SendComcheckRequestAsync(this.Trip.ID, ComcheckRequestType.FuelAdvance);
                    this.FuelState = FuelAdvanceStates.Requested;
                    this.FuelStartRequestedTimer();
                }
                catch (Exception exception)
                {
                    // To do: show exception message
                    Console.WriteLine(exception);
                }
                finally
                {
                    this.IsBusy = false;
                    this.EnabledCommands();
                }
            });
        }

        private void FuelResend(object parameter)
        {
            Task.Run(async () =>
            {
                this.DisableCommands();
                this.IsBusy = true;
                try
                {
					this.FuelStopRequestedTimer();
					await TrukmanContext.CancelComcheckRequestAsync(this.Trip.ID, ComcheckRequestType.FuelAdvance);
					await TrukmanContext.SendComcheckRequestAsync(this.Trip.ID, ComcheckRequestType.FuelAdvance);
					
					this.FuelState = FuelAdvanceStates.Requested;
					this.FuelStartRequestedTimer();
                }
                catch (Exception exception)
                {
                    // To do: Show exception message
                    Console.WriteLine(exception);
                }
                finally
                {
                    this.IsBusy = false;
                    this.EnabledCommands();
                }

            });
        }

        private void FuelCancel(object parameter)
        {
            Task.Run(async () =>
            {
                //this.FuelStopRequestedTimer();

                this.DisableCommands();
                this.IsBusy = true;
                try
                {
                    // To do: Cancel request
                    //Thread.Sleep(2000);
				    await TrukmanContext.CancelComcheckRequestAsync (this.Trip.ID, ComcheckRequestType.FuelAdvance);
					this.FuelState = FuelAdvanceStates.None;
					this.FuelStopRequestedTimer();
				}
                catch (Exception exception)
                {
                    // To do: Show exception message
                    Console.WriteLine(exception);
                }
                finally
                {
                    this.IsBusy = false;
                    this.EnabledCommands();
                }
            });
        }

        private  void FuelStartRequestedTimer()
        {
			if (_fuelRequestedTimer == null) {
				_fuelRequestedTimer = new System.Timers.Timer { Interval = 5000 };
				_fuelRequestedTimer.Elapsed += (sender, args) => {
					this.CheckFuelComcheck ();
				};
			}
            _fuelRequestedTimer.Start();
        }

		private async void CheckFuelComcheck ()
		{
			this.DisableCommands();
			this.IsBusy = true;
			try
			{
				var comcheckState = await TrukmanContext.GetComcheckStateAsync (this.Trip.ID, ComcheckRequestType.FuelAdvance);
				if (comcheckState == ComcheckRequestState.None)
				{
					this.FuelStopRequestedTimer();
					this.FuelStopReceivedTimer();
					this.FuelState = FuelAdvanceStates.None;
				}
				if (comcheckState == ComcheckRequestState.Received) {
					this.FuelStopRequestedTimer ();
					this.FuelState = FuelAdvanceStates.Received;
					this.FuelStartReceivedTimer ();
				}
				else if (comcheckState == ComcheckRequestState.Requested)
				{
					//this.FuelStopReceivedTimer();
					this.FuelState = FuelAdvanceStates.Requested;
					this.FuelStartRequestedTimer ();
				}
				else if (comcheckState == ComcheckRequestState.Visible)
				{
					var comcheck = await TrukmanContext.GetComcheckAsync (this.Trip.ID, ComcheckRequestType.FuelAdvance);
					if (!string.IsNullOrEmpty(comcheck))
					{
						this.FuelStopReceivedTimer ();
						this.FuelState = FuelAdvanceStates.Completed;
						this.FuelComcheck = comcheck;
					}
				}
			}
			catch (Exception exception)
			{
				// To do: show exception message
				Console.WriteLine(exception);
			}
			finally
			{
				this.IsBusy = false;
				this.EnabledCommands();
			}
		}

		private async void CheckLumperComcheck ()
		{
			this.DisableCommands();
			this.LumperIsBusy = true;
			try
			{
				var comcheckState = await TrukmanContext.GetComcheckStateAsync (this.Trip.ID, ComcheckRequestType.Lumper);
				if (comcheckState == ComcheckRequestState.None)
				{
					this.LumperStopRequestedTimer();
					this.LumperStopReceivedTimer();
					this.LumperState = LumperStates.None;
				}
				if (comcheckState == ComcheckRequestState.Received) {
					this.LumperStopRequestedTimer ();
					this.LumperState = LumperStates.Received;
					this.LumperStartReceivedTimer ();
				}
				else if (comcheckState == ComcheckRequestState.Requested)
				{
					//this.LumperStopReceivedTimer();
					this.LumperState = LumperStates.Requested;
					this.LumperStartRequestedTimer ();
				}
				else if (comcheckState == ComcheckRequestState.Visible)
				{
					var comcheck = await TrukmanContext.GetComcheckAsync(this.Trip.ID, ComcheckRequestType.Lumper);
					if (!string.IsNullOrEmpty(comcheck))
					{
						this.LumperStopReceivedTimer ();
						this.LumperState = LumperStates.Completed;
						this.LumperComcheck = comcheck;
					}
				}
			}
			catch (Exception exception)
			{
				// To do: show exception message
				Console.WriteLine(exception);
			}
			finally
			{
				this.LumperIsBusy = false;
				this.EnabledCommands();
			}

			//this.FuelStartReceivedTimer();
		}

        private void FuelStopRequestedTimer()
        {
            if (_fuelRequestedTimer != null)
                _fuelRequestedTimer.Stop();
        }

        private void FuelStartReceivedTimer()
        {
			if (_fuelReceivedTimer == null) {
				_fuelReceivedTimer = new System.Timers.Timer { Interval = 5000 };
				_fuelReceivedTimer.Elapsed += (sender, args) => {

					// To do: Check completed
					//Thread.Sleep(2000);
					CheckFuelComcheck ();
				};
			}
            _fuelReceivedTimer.Start();
        }

        private void FuelStopReceivedTimer()
        {
            if (_fuelReceivedTimer != null)
                _fuelReceivedTimer.Stop();
        }

        private void LumperSetStateText()
        {
            var stateText = "";
            var stateInfoText = "";
            if (this.LumperState == LumperStates.None)
            {
                stateText = AppLanguages.CurrentLanguage.LumperNoneLabel;
                stateInfoText = "";
            }
            else if (this.LumperState == LumperStates.Requested)
            {
                stateText = AppLanguages.CurrentLanguage.LumperRequestedLabel;
                stateInfoText = "";
            }
            else if (this.LumperState == LumperStates.Received)
            {
                stateText = AppLanguages.CurrentLanguage.LumperReceivedLabel;
                stateInfoText = AppLanguages.CurrentLanguage.LumperReceivedInfoLabel;
            }
            else if (this.LumperState == LumperStates.Completed)
            {
                stateText = String.Format(AppLanguages.CurrentLanguage.LumperCompletedLabel, this.LumperComcheck);
                stateInfoText = "";
            }
            this.LumperStateText = stateText;
            this.LumperStateInfoText = stateInfoText;
        }

        private void LumperRequest(object parameter)
        {
            Task.Run(async () => {
                this.DisableCommands();
                this.LumperIsBusy = true;
                try
                {
					await TrukmanContext.SendComcheckRequestAsync(this.Trip.ID, ComcheckRequestType.Lumper);
					this.LumperState = LumperStates.Requested;
					this.LumperStartRequestedTimer();
                }
                catch (Exception exception)
                {
                    // To do: show exception message
                    Console.WriteLine(exception);
                }
                finally
                {
                    this.LumperIsBusy = false;
                    this.EnabledCommands();
                }
            });
        }

        private void LumperResend(object parameter)
        {
            Task.Run(async () =>
            {
                this.DisableCommands();
                this.LumperIsBusy = true;
                try
                {
					this.LumperStopRequestedTimer();
					await TrukmanContext.CancelComcheckRequestAsync(this.Trip.ID, ComcheckRequestType.Lumper);
					await TrukmanContext.SendComcheckRequestAsync(this.Trip.ID, ComcheckRequestType.Lumper);

					this.LumperState = LumperStates.Requested;
					this.LumperStartRequestedTimer();
				}
                catch (Exception exception)
                {
                    // To do: Show exception message
                    Console.WriteLine(exception);
                }
                finally
                {
                    this.LumperIsBusy = false;
                    this.EnabledCommands();
                }
			});
		}

        private void LumperCancel(object parameter)
        {
            Task.Run(async () =>
            {
                this.DisableCommands();
                this.LumperIsBusy = true;
                try
                {
					await TrukmanContext.CancelComcheckRequestAsync(this.Trip.ID, ComcheckRequestType.Lumper);
					this.LumperState = LumperStates.None;
					this.LumperStopRequestedTimer();
                }
                catch (Exception exception)
                {
                    // To do: Show exception message
                    Console.WriteLine(exception);
                }
                finally
                {
                    this.LumperIsBusy = false;
                    this.EnabledCommands();
                }
			});
        }

        private void LumperStartRequestedTimer()
        {
			if (_lumperRequestedTimer == null) {
				_lumperRequestedTimer = new System.Timers.Timer { Interval = 5000 };
				_lumperRequestedTimer.Elapsed += (sender, args) => {
					this.CheckLumperComcheck();
				};
			}
            _lumperRequestedTimer.Start();
        }

        private void LumperStopRequestedTimer()
        {
            if (_lumperRequestedTimer != null)
                _lumperRequestedTimer.Stop();
        }

        private void LumperStartReceivedTimer()
        {
			if (_lumperReceivedTimer == null) {
				_lumperReceivedTimer = new System.Timers.Timer { Interval = 5000 };
				_lumperReceivedTimer.Elapsed += (sender, args) => {
					this.CheckLumperComcheck();
				};
			}
            _lumperReceivedTimer.Start();
        }

        private void LumperStopReceivedTimer()
        {
            if (_lumperReceivedTimer != null)
                _lumperReceivedTimer.Stop();
        }

		public ITrip Trip { get; private set; }

        public string FuelComcheck
        {
            get { return (string)this.GetValue("FuelComcheck"); }
            set { this.SetValue("FuelComcheck", value); }
        }

        public FuelAdvanceStates FuelState
        {
            get { return (FuelAdvanceStates)this.GetValue("FuelState", FuelAdvanceStates.None); }
            set { this.SetValue("FuelState", value); }
        }

        public string FuelStateText
        {
            get { return (string)this.GetValue("FuelStateText", AppLanguages.CurrentLanguage.FuelAdvanceNoneLabel); }
            set { this.SetValue("FuelStateText", value); }
        }

        public string FuelStateInfoText
        {
            get { return (string)this.GetValue("FuelStateInfoText"); }
            set { this.SetValue("FuelStateInfoText", value); }
        }

        public bool FuelNoneButtonVisible
        {
            get { return (bool)this.GetValue("FuelNoneButtonVisible", true); }
            set { this.SetValue("FuelNoneButtonVisible", value); }
        }

        public bool FuelRequestedButtonsVisible
        {
            get { return (bool)this.GetValue("FuelRequestedButtonsVisible", false); }
            set { this.SetValue("FuelRequestedButtonsVisible", value); }
        }

        public bool FuelReceivedTextInfoVisible
        {
            get { return (bool)this.GetValue("FuelReceivedTextInfoVisible", false); }
            set { this.SetValue("FuelReceivedTextInfoVisible", value); }
        }

        public string LumperComcheck
        {
            get { return (string)this.GetValue("LumperComcheck"); }
            set { this.SetValue("LumperComcheck", value); }
        }

        public LumperStates LumperState
        {
            get { return (LumperStates)this.GetValue("LumperState", LumperStates.None); }
            set { this.SetValue("LumperState", value); }
        }

        public string LumperStateText
        {
            get { return (string)this.GetValue("LumperStateText", AppLanguages.CurrentLanguage.FuelAdvanceNoneLabel); }
            set { this.SetValue("LumperStateText", value); }
        }

        public string LumperStateInfoText
        {
            get { return (string)this.GetValue("LumperStateInfoText"); }
            set { this.SetValue("LumperStateInfoText", value); }
        }

        public bool LumperNoneButtonVisible
        {
            get { return (bool)this.GetValue("LumperNoneButtonVisible", true); }
            set { this.SetValue("LumperNoneButtonVisible", value); }
        }

        public bool LumperRequestedButtonsVisible
        {
            get { return (bool)this.GetValue("LumperRequestedButtonsVisible", false); }
            set { this.SetValue("LumperRequestedButtonsVisible", value); }
        }

        public bool LumperReceivedTextInfoVisible
        {
            get { return (bool)this.GetValue("LumperReceivedTextInfoVisible", false); }
            set { this.SetValue("LumperReceivedTextInfoVisible", value); }
        }

        public bool LumperIsBusy
        {
            get { return (bool)this.GetValue("LumperIsBusy", false); }
            set { this.SetValue("LumperIsBusy", value); }
        }

        public VisualCommand ShowMainMenuCommand { get; private set; }

        public VisualCommand ShowHomePageCommand { get; private set; }

        public VisualCommand FuelRequestCommand { get; private set; }

        public VisualCommand FuelResendCommand { get; private set; }

        public VisualCommand FuelCancelCommand { get; private set; }

        public VisualCommand LumperRequestCommand { get; private set; }

        public VisualCommand LumperResendCommand { get; private set; }

        public VisualCommand LumperCancelCommand { get; private set; }
    }
    #endregion
}
