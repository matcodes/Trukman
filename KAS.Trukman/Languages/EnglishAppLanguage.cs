using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Languages
{
    #region EnglishAppLanguage
    public class EnglishAppLanguage : AppLanguage
    {
        public EnglishAppLanguage() 
            : base("English")
        {
            this.AppName = "TRUKMAN";

            #region Page titles
            this.TripPageName = "TRIP";
            this.ShipperInfoPageName = "SHIPPER INFO";
            this.ReceiverInfoPageName = "RECEIVER INFO";
            this.AdvancesPageName = "ADVANCES";
            this.FuelAdvancePageName = "FUEL ADVANCE";
            this.LumperPageName = "LUMPER";
            this.DelayEmergencyPageName = "DELAY/EMERGENCY";
            this.RoutePageName = "ROUTE";
            this.PointsAndRewardsPageName = "POINTS & REWARDS";
            this.SettingsPageName = "SETTINGS";
            this.HelpPageName = "HELP";
            #endregion

            #region Main menu
            this.MainMenuHomeLabel = "Home";
            this.MainMenuTripLabel = "Trip";
            this.MainMenuAdvancesLabel = "Advances";
            this.MainMenuDelayEmergencyLabel = "Delay/Emergency";
            this.MainMenuRouteLabel = "Route";
            this.MainMenuPointsAndRewardsLabel = "Points & Rewards";
            this.MainMenuSettingsLabel = "Settings";
            this.MainMenuHelpLabel = "Help";
            #endregion

            #region Home page
            this.HomeWaitingForTripLabel = "Waiting for your next trip to be assigned to you";
            this.HomeNextTripLabel = "Your Next Trip";
            this.HomeOriginLabel = "Origin";
            this.HomeDestinationLabel = "Destination";
            this.HomePointsLabel = "Points: {0}";
            this.HomeDeclinedLabel = "Declined";
            this.HomeCancelledTripLabel = "The previously scheduled trip has been cancelled.";
            this.HomeArrivedOnTimeLabel = "Congrats for arriving on time! You received";
            this.HomeArrivedOnTimeBonusPointsLabel = "50 pts";
            this.HomeArrivedOnTimeBonusPointsMinsLabel = "Plus 5 points for arriving 15 mins early";
            this.HomeTotalPointsLabel = "Total Points: {0}";
            this.HomeBonusPointsForPhotoLabel = "5 Points for photo of bill of lading";
            this.HomeBonusPointsForTimeLabel = "5 Points for completing load by 4:00 PM";
            this.HomeArrivedLateLabel = "You have arrived at the pickup location.";
            this.HomeArrivedLateBonusLabel = "10 points have been deducted for late arrival.";
            this.HomeDeclinedReason_1 = "Reason 1";
            this.HomeDeclinedReason_2 = "Reason 2";
            this.HomeDeclinedOtherReason = "Other";
            this.HomeDeclinedOtherReasonPlaceholder = "Memo";
            this.HomeDeclineButtonText = "Decline";
            this.HomeAcceptButtonText = "Accept";
            this.HomeSubmitButtonText = "Submit";
            this.HomeContinueButtonText = "Continue";
            this.HomeDeclinedSubmitErrorText = "Please provide your reason in the Memo";

            this.HomeGPSPopupMainLabel = "Turn on Location Services to allow Trukman to determine your location";
            this.HomeGPSPopupSmallerLabel = "Trukman needs your location to work properly";
            this.HomeGPSPopupSettingsButtonText = "Settings";
            this.HomeGPSPopupCancelButtonText = "Cancel";

			this.HomeCongratulations = "Congratulations!";
			this.HomeRewardsButtonText = "Rewards";
            #endregion

            #region Trip page
            this.TripShipperTitleLabel = "Shipper";
            this.TripReceiverTitleLabel = "Receiver";
            #endregion

            #region Contractor page
            this.ContractorPageNameLabel = "Name:";
            this.ContractorPagePhoneLabel = "Tel#:";
            this.ContractorPageFaxLabel = "Fax#:";
            this.ContractorPageAddressLabel = "Address:";
            #endregion

            #region Fuel advance page
            this.FuelAdvanceNoneLabel = "No Fuel Comcheck issued";
            this.FuelAdvanceRequestedLabel = "Requested";
            this.FuelAdvanceReceivedLabel = "Fuel Comcheck Received";
            this.FuelAdvanceReceivedInfoLabel = "The Comcheck will be visible once Dispatch receives your Bill of Lading";
            this.FuelAdvanceCompletedLabel = "Fuel Comcheck: {0}";

            this.FuelAdvanceNoneRequestButtonText = "Request Fuel Comcheck";
            this.FuelAdvanceReceivedResendButtonText = "Resend";
            this.FuelAdvanceReceivedCancelButtonText = "Cancel";
            #endregion

            #region Lumper page
            this.LumperNoneLabel = "No Lumper Comcheck issued";
            this.LumperRequestedLabel = "Requested";
            this.LumperReceivedLabel = "Lumper Comcheck Received";
            this.LumperReceivedInfoLabel = "The Comcheck will be visible once Dispatch receives your Bill of Lading";
            this.LumperCompletedLabel = "Lumper Comcheck: {0}";

            this.LumperNoneRequestButtonText = "Request Lumper Comcheck";
            this.LumperReceivedResendButtonText = "Resend";
            this.LumperReceivedCancelButtonText = "Cancel";
            #endregion

            #region Delay / Emergency page
            this.DelaySelectTypeLabel = "Select Type";
            this.DelayFlatTireLabel = "Flat Tire";
            this.DelayFeelingSleepyLabel = "Feeling Sleepy";
            this.DelayRoadWorkAheadLabel = "Road Work Ahead";
            this.DelayCommentsPlaceholderText = "Comments";
            this.DelaySubmitButtonText = "Submit";
            #endregion

            #region Months
            this.January = "January";
            this.February = "February";
            this.March = "March";
            this.April = "April";
            this.May = "May";
            this.June = "June";
            this.July = "July";
            this.August = "August";
            this.September = "September";
            this.October = "October";
            this.November = "November";
            this.December = "December";
            #endregion

            #region Times
            this.TimeAM = "AM";
            this.TimePM = "PM";
            #endregion
     
			#region Sign up
			this.SignUpLabel = "Sign up";
			#endregion
		}
    }
    #endregion
}
