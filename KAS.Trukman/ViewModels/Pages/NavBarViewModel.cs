using KAS.Trukman.Classes;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.ViewModels.Pages
{
    #region NavBarViewModel
    public class NavBarViewModel : PageViewModel
    {
        public NavBarViewModel() 
            : base()
        {
            this.ShowTripCommand = new VisualCommand(this.ShowTrip);
            this.ShowShipperInfoCommand = new VisualCommand(this.ShowShipperInfo);
            this.ShowReceiverInfoCommand = new VisualCommand(this.ShowReceiverInfo);
            this.ShowFuelAdvanceCommand = new VisualCommand(this.ShowFuelAdvance);
            this.ShowLumperCommand = new VisualCommand(this.ShowLumper);
            this.ShowDelayEmergencyCommand = new VisualCommand(this.ShowDelayEmergency);
        }

        public override void Appering()
        {
            base.Appering();
        }

        public override void Disappering()
        {
            base.Disappering();
        }

        protected override void Localize()
        {
            this.Title = AppLanguages.CurrentLanguage.AppName;
        }

        private void ShowTrip(object parameter)
        {
            var trip = new Trip {
                Shipper = new Contractor {
                    Name = "ACME Shipping, Inc.",
                    Phone = "123-456-789",
                    Fax = "123-456-789",
                    Address = "10777 Santa Monica Blvd, Los Angeles, California"
                },
                Receiver = new Contractor {
                    Name = "ACME Shipping, Inc.",
                    Phone = "123-456-789",
                    Fax = "123-456-789",
                    Address = "10777 Santa Monica Blvd, Los Angeles, California"
                }
            };
            ShowTripPageMessage.Send(trip);
        }

        private void ShowShipperInfo(object parameter)
        {
            var shipper = new Contractor {
                Name = "ACME Shipping, Inc.",
                Phone = "123-456-789",
                Fax = "123-456-789",
                Address = "10777 Santa Monica Blvd, Los Angeles, California"
            };
            ShowShipperInfoPageMessage.Send(shipper);
        }

        private void ShowReceiverInfo(object parameter)
        {
            var receiver = new Contractor {
                Name = "ACME Shipping, Inc.",
                Phone = "123-456-789",
                Fax = "123-456-789",
                Address = "10777 Santa Monica Blvd, Los Angeles, California"
            };
            ShowReceiverInfoPageMessage.Send(receiver);
        }

        private void ShowFuelAdvance(object parameter)
        {
            ShowFuelAdvancePageMessage.Send();
        }

        private void ShowLumper(object parameter)
        {
            ShowLumperPageMessage.Send();
        }

        private void ShowDelayEmergency(object parameter)
        {
			//ShowDelayEmergencyPageMessage.Send();
        }

        public VisualCommand ShowTripCommand { get; private set; }

        public VisualCommand ShowShipperInfoCommand { get; private set; }

        public VisualCommand ShowReceiverInfoCommand { get; private set; }

        public VisualCommand ShowFuelAdvanceCommand { get; private set; }

        public VisualCommand ShowLumperCommand { get; private set; }

        public VisualCommand ShowDelayEmergencyCommand { get; private set; }
    }
    #endregion
}
