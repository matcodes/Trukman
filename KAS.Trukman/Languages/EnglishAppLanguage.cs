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
            this.SignUpPageName = "SIGN UP";
            this.OwnerFleetPageName = "FLEET";
            this.OwnerFuelAdvancePageName = "FUEL ADVANCE";
            this.OwnerLumperPageName = "LUMPER";
            this.OwnerBrokerListPageName = "BROKER LIST";
            this.OwnerInvoiceListPageName = "INVOICE";
            this.OwnerInvoiceViewerPageName = "INVOICE";
            this.OwnerDeliveryUpdatePageName = "UPDATES";
			this.OwnerDelayAlertsPageName = "DELAY ALERTS";
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
            this.DriverAuthorizePageName = "DRIVER AUTHORIZATION";
            #endregion

			#region Lists
			this.AdvanceListJobNumberLabel = "Job#:";
			this.AdvanceListDriverNameLabel = "Driver:";
            this.InvoiceListJobNumberLabel = "Job#:";
            this.InvoiceListDriverNameLabel = "Driver:";
			this.JobAlertListJobNumberLabel = "Job#:";
			this.JobAlertListDriverNameLabel = "Driver:";
            #endregion

            #region ComcheckPopup
            this.ComcheckPopupEntryPlaceholer = "Comcheck";
			this.ComcheckPopupCancelButtonText = "Cancel";
			this.ComcheckPopupAcceptButtonText = "Accept";
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

            #region Owner main menu
            this.OwnerMainMenuManageDriversLabel = "Manage Drivers";
            this.OwnerMainMenuManageDispatchersLabel = "Manage Dispatchers";
            this.OwnerMainMenuManageFleetLabel = "Manage Fleet";
            this.OwnerMainMenuSettingsLabel = "Settings";
            this.OwnerMainMenuHelpLabel = "Help";
            this.OwnerMainMenuSelectLanguageLabel = "Cambiar a Espanol";
            this.OwnerMainMenuSearchTextPlaceholder = "Search";
            #endregion

            #region Owner Home page
            this.OwnerHomeBrockerListCommandItemLabel = "Brocker List";
            this.OwnerHomeDelayAlertsCommandItem = "Delay Alerts";
            this.OwnerHomeDeliveryUpdateCommandItemLabel = "Delivery Update";
            this.OwnerHomeDispatchDriverCommandItemLabel = "Dispatch Driver";
            this.OwnerHomeFuelAdvanceCommandItemLabel = "Fuel Advance";
            this.OwnerHomeInvoiceCommandItemLabel = "Invoice";
            this.OwnerHomeLoadConfirmationCommandItemLabel = "Load Confirmation";
            this.OwnerHomeLumperCommandItemLabel = "Lumper";
            this.OwnerHomeRateConfirmationCommandItemLabel = "Rate Confirmation";
            this.OwnerHomeReportsCommandItemLabel = "Reports";
            this.OwnerHomeTrackFleetCommandItemLabel = "Track Fleet";
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
            this.HomeNextStepLabel = "Next Step";
            this.HomeBonusPointsForPickupPhotoLabel = "Send photo of Bill of Lading";
            this.HomeBonusPointsForDeliveryPhotoLabel = "Send photo of Delivery Proof";
            this.HomeBonusPointsForTimeLabel = "(5 Points for completing load by 4:00 PM)";
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
			this.HomeNewTripButtonText = "New Trip";
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
            this.SignUpEnglishLanguageLabel = "ENG";
            this.SignUpEspanolLanguageLabel = "ESP";
            this.SignUpMainLabel = "SIGN UP AS";
            this.SignUpDriverLabel = "DRIVER";
            this.SignUpDispatcherLabel = "DISPATCH";
            this.SignUpOwnerLabel = "OWNER/OPERATOR";
            this.SignUpDriverPendingLabel = "Waiting for {0} to authorize you";
            this.SignUpDriverDeclinedLabel = "{0} has declined your authorization request";
            this.SignUpDriverAuthorizedLabel = "You have been authorized by {0}";

            this.SignUpLabel = "SIGN UP";
            this.SignUpSelectCompanyAcceptButtonText = "Accept";
            this.SignUpSelectCompanyCancelButtonText = "Cancel";
            this.SignUpSubmitButtonText = "Submit";
            this.SignUpContinueButtonText = "Continue";
            this.SignUpCancelAuthorizationButtonText = "Cancel Authorization Request";

            this.SignUpMCCodePlaceholder = "MC#";
            this.SignUpCompanyNamePlaceholder = "Name";
            this.SignUpCompanyDBAPlaceholder = "DBA";
            this.SignUpCompanyAddressPlaceholder = "Physical Address";
            this.SignUpCompanyPhonePlaceholder = "Phone";
            this.SignUpCompanyEMailPlaceholder = "EMail";
            this.SignUpCompanyFleetSizePlaceholder = "Fleet Size";
            this.SignUpSelectCompanySearchPlaceholder = "Search company";
            this.SignUpDriverFirstNamePlaceholder = "First Name";
            this.SignUpDriverLastNamePlaceholder = "Last Name";
            this.SignUpDriverPhonePlaceholder = "Phone";
            this.SignUpDriverCompanyNamePlaceholder = "Company Name";

            this.SignUpMCExceededMessageText = "Sorry you are having trouble signing up. Please email help@trukman.com for assistance.";

            this.SignUpMCNotFoundErrorMessageText = "MC# not found.";
            this.SignUpCompanyPhoneEmptyErrorMessageText = "Phone# cannot be blank.";
            this.SignUpCompanyIncorectEMailErrorMessageText = "Email is incorrect.";
            this.SignUpCompanyFleetSizeErrorMessageText = "Fleet should be a number.";
            #endregion

            #region Driver authorization page
            this.DriverAuthorizationCommonLabel = "{0} {1} has requested your authorization to use Trukman as a driver.";
            this.DriverAuthorizationAssignIDNumberPlaceholder = "Assign an ID#";
            this.DriverAuthorizationAuthorizeButtonText = "Authorize";
            this.DriverAuthorizationDeclineButtonText = "Decline";
            #endregion

            #region SignUpOwnerWelcome page
            this.SignUpOwnerWelcomeLabel = "Welcome to {0}!";
            this.SignUpOwnerWelcomeContinueButtonText = "Continue";
            #endregion

            #region SignUp user roles
            this.SignUpUserRoleOwnerOperator = "OWNER/OPERATOR";
            #endregion

            #region Error messages
            this.CheckInternetConnectionErrorMessage = "Check internet connection.";
            #endregion

            #region System messages
            this.FindNextTripSystemMessage = "You have a new job offer";
            this.TripCancelledSystemMessage = "Job is canceled";
            this.ArrivedToPickupSystemMessage = "You reached the pick up location";
            this.ArrivedToDeliverySystemMessage = "You reached the drop off location";

            this.OwnerArrivedToPickupSystemMessage = "Job # {0}. {1} has arrived to pick up place";
            this.OwnerArrivedToDeliverySystemMessage = "Job # {0}. {1} has arrived to drop off place";
            this.OwnerFuelRequestedSystemMessage = "Job # {0}. {1} requested a fuel advance";
            this.OwnerLumperRequestedSystemMessage = "Job # {0}. {1} requested a lumper advance";
            #endregion
        }
    }
    #endregion
}
