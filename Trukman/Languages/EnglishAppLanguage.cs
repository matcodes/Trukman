using System;
using System.Collections.Generic;
using System.Text;

namespace Trukman.Languages
{
    #region EnglishAppLanguage
    public class EnglishAppLanguage : AppLanguage
    {
        public EnglishAppLanguage() 
            : base("English")
        {
            this.AppName = "TRUKMAN";

            this.ShipperInfoPageName = "SHIPPER INFO";
            this.ReceiverInfoPageName = "RECEIVER INFO";
            this.FuelAdvancePageName = "FUEL ADVANCE";

            this.ContractorPageNameLabel = "Name:";
            this.ContractorPagePhoneLabel = "Tel#:";
            this.ContractorPageFaxLabel = "Fax#:";
            this.ContractorPageAddressLabel = "Address:";

            this.FuelAdvanceNoneLabel = "No Fuel Comcheck issued";
            this.FuelAdvanceRequestedLabel = "Requested";
            this.FuelAdvanceReceivedLabel = "Fuel Comcheck Received";
            this.FuelAdvanceReceivedInfoLabel = "The Comcheck will be visible once Dispatch receives your Bill of Lading";
            this.FuelAdvanceCompletedLabel = "Fuel Comcheck {0}";

            this.FuelAdvanceNoneRequestButtonText = "Request Fuel Comcheck";
            this.FuelAdvanceReceivedResendButtonText = "Resend";
            this.FuelAdvanceReceivedCancelButtonText = "Cancel";

			this.TrukmanLabel = "Trukman";
			this.DriverListLabel = "Driver list";

			this.SignUpLabel = "Sign up";
        }
    }
    #endregion
}
