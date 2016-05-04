using KAS.Trukman.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using KAS.Trukman.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;

namespace KAS.Trukman.ViewModels.Pages
{
    #region FuelAdvanceViewModel
    public class FuelAdvanceViewModel : PageViewModel
    {
        private System.Timers.Timer _requestedTimer = null;
        private System.Timers.Timer _receivedTimer = null;

        public FuelAdvanceViewModel()
            : base()
        {
            this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);
            this.ShowPrevPageCommand = new VisualCommand(this.ShowPrevPage);
            this.RequestCommand = new VisualCommand(this.Request);
            this.ResendCommand = new VisualCommand(this.Resend);
            this.CancelCommand = new VisualCommand(this.Cancel);
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);

            // To do: get comcheck
            this.Comcheck = "";
        }

        public override void Appering()
        {
            base.Appering();
        }

        public override void Disappering()
        {
            this.StopReceivedTimer();
            this.StopRequestedTimer();

            base.Disappering();
        }

        protected override void DoPropertyChanged(string propertyName)
        {
            Device.BeginInvokeOnMainThread(() => 
            {
                if (propertyName == "State")
                {
                    this.SetStateText();
                    this.NoneButtonVisible = (this.State == FuelAdvanceStates.None);
                    this.RequestedButtonsVisible = (this.State == FuelAdvanceStates.Requested);
                    this.ReceivedTextInfoVisible = (this.State == FuelAdvanceStates.Received);
                }
            });

            base.DoPropertyChanged(propertyName);
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.FuelAdvancePageName;
            this.SetStateText();
        }

        protected override void DisableCommands()
        {
            base.DisableCommands();

            Device.BeginInvokeOnMainThread(() => 
            {
                this.ShowHomePageCommand.IsEnabled = false;
                this.ShowPrevPageCommand.IsEnabled = false;
                this.RequestCommand.IsEnabled = false;
                this.ResendCommand.IsEnabled = false;
                this.CancelCommand.IsEnabled = false;
            });
        }

        protected override void EnabledCommands()
        {
            base.EnabledCommands();

            Device.BeginInvokeOnMainThread(() => 
            {
                this.ShowHomePageCommand.IsEnabled = true;
                this.ShowPrevPageCommand.IsEnabled = true;
                this.RequestCommand.IsEnabled = true;
                this.ResendCommand.IsEnabled = true;
                this.CancelCommand.IsEnabled = true;
            });
        }

        private void SetStateText()
        {
            var stateText = "";
            var stateInfoText = "";
            if (this.State == FuelAdvanceStates.None)
            {
                stateText = AppLanguages.CurrentLanguage.FuelAdvanceNoneLabel;
                stateInfoText = "";
            }
            else if (this.State == FuelAdvanceStates.Requested)
            {
                stateText = AppLanguages.CurrentLanguage.FuelAdvanceRequestedLabel;
                stateInfoText = "";
            }
            else if (this.State == FuelAdvanceStates.Received)
            {
                stateText = AppLanguages.CurrentLanguage.FuelAdvanceReceivedLabel;
                stateInfoText = AppLanguages.CurrentLanguage.FuelAdvanceReceivedInfoLabel;
            }
            else if (this.State == FuelAdvanceStates.Completed)
            {
                stateText = String.Format(AppLanguages.CurrentLanguage.FuelAdvanceCompletedLabel, this.Comcheck);
                stateInfoText = "";
            }
            this.StateText = stateText;
            this.StateInfoText = stateInfoText;
        }

        private void ShowHomePage(object parameter)
        {
            ShowMainMenuMessage.Send();
        }

        private void ShowPrevPage(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        private void Request(object parameter)
        {
            Task.Run(() => {
                this.DisableCommands();
                this.IsBusy = true;
                try
                {
                    Thread.Sleep(2000);
                    this.State = FuelAdvanceStates.Requested;
                    this.StartRequestedTimer();
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

        private void Resend(object parameter)
        {
            Task.Run(() => 
            {
                this.StopRequestedTimer();

                this.DisableCommands();
                this.IsBusy = true;
                try
                {
                    // To do: Resend request
                    Thread.Sleep(2000);
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

                this.State = FuelAdvanceStates.Requested;
                this.StartRequestedTimer();
            });
        }

        private void Cancel(object parameter)
        {
            Task.Run(() => 
            {
                this.StopRequestedTimer();

                this.DisableCommands();
                this.IsBusy = true;
                try
                {
                    // To do: Cancel request
                    Thread.Sleep(2000);
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

                this.State = FuelAdvanceStates.None;
            });
        }

        private void StartRequestedTimer()
        {
            _requestedTimer = new System.Timers.Timer { Interval = 5000 };
            _requestedTimer.Elapsed += (sender, args) => 
            {
                this.StopRequestedTimer();

                // To do: check receive
                this.DisableCommands();
                this.IsBusy = true;
                try
                {
                    Thread.Sleep(2000);
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

                this.State = FuelAdvanceStates.Received;

                this.StartReceivedTimer();
            };
            _requestedTimer.Start();
        }

        private void StopRequestedTimer()
        {
            if (_requestedTimer != null)
                _requestedTimer.Stop();
        }

        private void StartReceivedTimer()
        {
            _receivedTimer = new System.Timers.Timer { Interval = 5000 };
            _receivedTimer.Elapsed += (sender, args) => {
                this.StopReceivedTimer();

                // To do: Check completed
                this.DisableCommands();
                this.IsBusy = true;
                try
                {
                    Thread.Sleep(2000);
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

                this.Comcheck = "0123456789";
                this.State = FuelAdvanceStates.Completed;
            };
            _receivedTimer.Start();
        }

        private void StopReceivedTimer()
        {
            if (_receivedTimer != null)
                _receivedTimer.Stop();
        }

        public string Comcheck
        {
            get { return (string)this.GetValue("Comcheck"); }
            set { this.SetValue("Comcheck", value); }
        }

        public FuelAdvanceStates State
        {
            get { return (FuelAdvanceStates)this.GetValue("State", FuelAdvanceStates.None); }
            set { this.SetValue("State", value); }
        }

        public string StateText
        {
            get { return (string)this.GetValue("StateText", AppLanguages.CurrentLanguage.FuelAdvanceNoneLabel); }
            set { this.SetValue("StateText", value); }
        }

        public string StateInfoText
        {
            get { return (string)this.GetValue("StateInfoText"); }
            set { this.SetValue("StateInfoText", value); }
        }

        public bool NoneButtonVisible
        {
            get { return (bool)this.GetValue("NoneButtonVisible", true); }
            set { this.SetValue("NoneButtonVisible", value); }
        }

        public bool RequestedButtonsVisible
        {
            get { return (bool)this.GetValue("RequestedButtonsVisible", false); }
            set { this.SetValue("RequestedButtonsVisible", value); }
        }

        public bool ReceivedTextInfoVisible
        {
            get { return (bool)this.GetValue("ReceivedTextInfoVisible", false); }
            set { this.SetValue("ReceivedTextInfoVisible", value); }
        }

        public VisualCommand ShowHomePageCommand { get; private set; }

        public VisualCommand ShowPrevPageCommand { get; private set; }

        public VisualCommand RequestCommand { get; private set; }

        public VisualCommand ResendCommand { get; private set; }

        public VisualCommand CancelCommand { get; private set; }
    }
    #endregion

    #region FuelAdvanceStates
    public enum FuelAdvanceStates
    {
        None = 0, 
        Requested = 1,
        Received = 2,
        Completed = 3
    }
    #endregion
}
