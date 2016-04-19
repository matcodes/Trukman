using KAS.Trukman.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Languages
{
    #region AppLanguage
    public class AppLanguage : BaseData
    {
        public AppLanguage(string displayName)
            : base()
        {
            this.DisplayName = displayName;
        }

        public void Assign(AppLanguage appLanguage)
        {
            if (appLanguage != null)
            {
                this.DisplayName = appLanguage.DisplayName;

                this.AppName = appLanguage.AppName;

                #region Page titles
                this.TripPageName = appLanguage.TripPageName;
                this.ShipperInfoPageName = appLanguage.ShipperInfoPageName;
                this.ReceiverInfoPageName = appLanguage.ReceiverInfoPageName;
                this.AdvancesPageName = appLanguage.AdvancesPageName;
                this.FuelAdvancePageName = appLanguage.FuelAdvancePageName;
                this.LumperPageName = appLanguage.LumperPageName;
                this.DelayEmergencyPageName = appLanguage.DelayEmergencyPageName;
                this.RoutePageName = appLanguage.RoutePageName;
                this.PointsAndRewardsPageName = appLanguage.PointsAndRewardsPageName;
                this.SettingsPageName = appLanguage.SettingsPageName;
                this.HelpPageName = appLanguage.HelpPageName;
                this.DriverAuthorizePageName = appLanguage.DriverAuthorizePageName;
                #endregion

                #region Main menu
                this.MainMenuHomeLabel = appLanguage.MainMenuHomeLabel;
                this.MainMenuTripLabel = appLanguage.MainMenuTripLabel;
                this.MainMenuAdvancesLabel = appLanguage.MainMenuAdvancesLabel;
                this.MainMenuDelayEmergencyLabel = appLanguage.MainMenuDelayEmergencyLabel;
                this.MainMenuRouteLabel = appLanguage.MainMenuRouteLabel;
                this.MainMenuPointsAndRewardsLabel = appLanguage.MainMenuPointsAndRewardsLabel;
                this.MainMenuSettingsLabel = appLanguage.MainMenuSettingsLabel;
                this.MainMenuHelpLabel = appLanguage.MainMenuHelpLabel;
                #endregion

                #region Home page
                this.HomeWaitingForTripLabel = appLanguage.HomeWaitingForTripLabel;
                this.HomeNextTripLabel = appLanguage.HomeNextTripLabel;
                this.HomeOriginLabel = appLanguage.HomeOriginLabel;
                this.HomeDestinationLabel = appLanguage.HomeDestinationLabel;
                this.HomePointsLabel = appLanguage.HomePointsLabel;
                this.HomeCancelledTripLabel = appLanguage.HomeCancelledTripLabel;
                this.HomeArrivedOnTimeLabel = appLanguage.HomeArrivedOnTimeLabel;
                this.HomeArrivedOnTimeBonusPointsLabel = appLanguage.HomeArrivedOnTimeBonusPointsLabel;
                this.HomeArrivedOnTimeBonusPointsMinsLabel = appLanguage.HomeArrivedOnTimeBonusPointsMinsLabel;
                this.HomeTotalPointsLabel = appLanguage.HomeTotalPointsLabel;
                this.HomeBonusPointsForPhotoLabel = appLanguage.HomeBonusPointsForPhotoLabel;
                this.HomeBonusPointsForTimeLabel = appLanguage.HomeBonusPointsForTimeLabel;
                this.HomeArrivedLateLabel = appLanguage.HomeArrivedLateLabel;
                this.HomeArrivedLateBonusLabel = appLanguage.HomeArrivedLateBonusLabel;
                this.HomeDeclinedLabel = appLanguage.HomeDeclinedLabel;
                this.HomeDeclinedReason_1 = appLanguage.HomeDeclinedReason_1;
                this.HomeDeclinedReason_2 = appLanguage.HomeDeclinedReason_2;
                this.HomeDeclinedOtherReason = appLanguage.HomeDeclinedOtherReason;
                this.HomeDeclinedOtherReasonPlaceholder = appLanguage.HomeDeclinedOtherReasonPlaceholder;
                this.HomeDeclineButtonText = appLanguage.HomeDeclineButtonText;
                this.HomeAcceptButtonText = appLanguage.HomeAcceptButtonText;
                this.HomeSubmitButtonText = appLanguage.HomeSubmitButtonText;
                this.HomeContinueButtonText = appLanguage.HomeContinueButtonText;
                this.HomeDeclinedSubmitErrorText = appLanguage.HomeDeclinedSubmitErrorText;

                this.HomeGPSPopupMainLabel = appLanguage.HomeGPSPopupMainLabel;
                this.HomeGPSPopupSmallerLabel = appLanguage.HomeGPSPopupSmallerLabel;
                this.HomeGPSPopupSettingsButtonText = appLanguage.HomeGPSPopupSettingsButtonText;
                this.HomeGPSPopupCancelButtonText = appLanguage.HomeGPSPopupCancelButtonText;

				this.HomeCongratulations = appLanguage.HomeCongratulations;
				this.HomeRewardsButtonText = appLanguage.HomeRewardsButtonText;
                #endregion

                #region Trip page
                this.TripReceiverTitleLabel = appLanguage.TripReceiverTitleLabel;
                this.TripShipperTitleLabel = appLanguage.TripShipperTitleLabel;
                #endregion

                #region Contractor page
                this.ContractorPageNameLabel = appLanguage.ContractorPageNameLabel;
                this.ContractorPagePhoneLabel = appLanguage.ContractorPagePhoneLabel;
                this.ContractorPageFaxLabel = appLanguage.ContractorPageFaxLabel;
                this.ContractorPageAddressLabel = appLanguage.ContractorPageAddressLabel;
                #endregion

                #region Fuel Advance page
                this.FuelAdvanceNoneLabel = appLanguage.FuelAdvanceNoneLabel;
                this.FuelAdvanceRequestedLabel = appLanguage.FuelAdvanceRequestedLabel;
                this.FuelAdvanceReceivedLabel = appLanguage.FuelAdvanceReceivedLabel;
                this.FuelAdvanceReceivedInfoLabel = appLanguage.FuelAdvanceReceivedInfoLabel;
                this.FuelAdvanceCompletedLabel = appLanguage.FuelAdvanceCompletedLabel;

                this.FuelAdvanceNoneRequestButtonText = appLanguage.FuelAdvanceNoneRequestButtonText;
                this.FuelAdvanceReceivedResendButtonText = appLanguage.FuelAdvanceReceivedResendButtonText;
                this.FuelAdvanceReceivedCancelButtonText = appLanguage.FuelAdvanceReceivedCancelButtonText;
                #endregion

                #region Lumper page
                this.LumperNoneLabel = appLanguage.LumperNoneLabel;
                this.LumperRequestedLabel = appLanguage.LumperRequestedLabel;
                this.LumperReceivedLabel = appLanguage.LumperReceivedLabel;
                this.LumperReceivedInfoLabel = appLanguage.LumperReceivedInfoLabel;
                this.LumperCompletedLabel = appLanguage.LumperCompletedLabel;

                this.LumperNoneRequestButtonText = appLanguage.LumperNoneRequestButtonText;
                this.LumperReceivedResendButtonText = appLanguage.LumperReceivedResendButtonText;
                this.LumperReceivedCancelButtonText = appLanguage.LumperReceivedCancelButtonText;
                #endregion

                #region Delay / Emergency page
                this.DelaySelectTypeLabel = appLanguage.DelaySelectTypeLabel;
                this.DelayFlatTireLabel = appLanguage.DelayFlatTireLabel;
                this.DelayFeelingSleepyLabel = appLanguage.DelayFeelingSleepyLabel;
                this.DelayRoadWorkAheadLabel = appLanguage.DelayRoadWorkAheadLabel;
                this.DelayCommentsPlaceholderText = appLanguage.DelayCommentsPlaceholderText;
                this.DelaySubmitButtonText = appLanguage.DelaySubmitButtonText;
                #endregion

                #region Months
                this.January = appLanguage.January;
                this.February = appLanguage.February;
                this.March = appLanguage.March;
                this.April = appLanguage.April;
                this.May = appLanguage.May;
                this.June = appLanguage.June;
                this.July = appLanguage.July;
                this.August = appLanguage.August;
                this.September = appLanguage.September;
                this.October = appLanguage.October;
                this.November = appLanguage.November;
                this.December = appLanguage.December;
                #endregion

                #region Times
                this.TimeAM = appLanguage.TimeAM;
                this.TimePM = appLanguage.TimePM;
                #endregion
            
				#region Sign up
				this.SignUpLabel = appLanguage.SignUpLabel;
                #endregion

                #region Driver authorization page
                this.DriverAuthorizationCommonLabel = appLanguage.DriverAuthorizationCommonLabel;
                this.DriverAuthorizationAssignIDNumberPlaceholder = appLanguage.DriverAuthorizationAssignIDNumberPlaceholder;
                this.DriverAuthorizationAuthorizeButtonText = appLanguage.DriverAuthorizationAuthorizeButtonText;
                this.DriverAuthorizationDeclineButtonText = appLanguage.DriverAuthorizationDeclineButtonText;
                #endregion

                #region SignUpOwnerWelcome page
                this.SignUpOwnerWelcomeLabel = appLanguage.SignUpOwnerWelcomeLabel;
                this.SignUpOwnerWelcomeContinueButtonText = appLanguage.SignUpOwnerWelcomeContinueButtonText;
                #endregion

                #region SignUp user roles
                this.SignUpUserRoleOwnerOperator = appLanguage.SignUpUserRoleOwnerOperator;
                #endregion
            }
        }

        public string DisplayName { get; private set; }

        public string AppName
        {
            get { return (string)this.GetValue("AppName"); }
            set { this.SetValue("AppName", value); }
        }

        #region Page names
        public string TripPageName
        {
            get { return (string)this.GetValue("TripPageName"); }
            set { this.SetValue("TripPageName", value); }
        }

        public string ShipperInfoPageName
        {
            get { return (string)this.GetValue("ShipperInfoPageName"); }
            set { this.SetValue("ShipperInfoPageName", value); }
        }

        public string ReceiverInfoPageName
        {
            get { return (string)this.GetValue("ReceiverInfoPageName"); }
            set { this.SetValue("ReceiverInfoPageName", value); }
        }

        public string AdvancesPageName
        {
            get { return (string)this.GetValue("AdvancesPageName"); }
            set { this.SetValue("AdvancesPageName", value); }
        }

        public string FuelAdvancePageName
        {
            get { return (string)this.GetValue("FuelAdvancePageName"); }
            set { this.SetValue("FuelAdvancePageName", value); }
        }

        public string LumperPageName
        {
            get { return (string)this.GetValue("LumperPageName"); }
            set { this.SetValue("LumperPageName", value); }
        }

        public string DelayEmergencyPageName
        {
            get { return (string)this.GetValue("DelayEmergencyPageName"); }
            set { this.SetValue("DelayEmergencyPageName", value); }
        }

        public string RoutePageName
        {
            get { return (string)this.GetValue("RoutePageName"); }
            set { this.SetValue("RoutePageName", value); }
        }

        public string PointsAndRewardsPageName
        {
            get { return (string)this.GetValue("PointsAndRewardsPageName"); }
            set { this.SetValue("PointsAndRewardsPageName", value); }
        }

        public string SettingsPageName
        {
            get { return (string)this.GetValue("SettingsPageName"); }
            set { this.SetValue("SettingsPageName", value); }
        }

        public string HelpPageName
        {
            get { return (string)this.GetValue("HelpPageName"); }
            set { this.SetValue("HelpPageName", value); }
        }

        public string DriverAuthorizePageName
        {
            get { return (string)this.GetValue("DriverAuthorizePageName"); }
            set { this.SetValue("DriverAuthorizePageName", value); }
        }
        #endregion

        #region Main Menu
        public string MainMenuHomeLabel
        {
            get { return (string)this.GetValue("MainMenuHomeLabel"); }
            set { this.SetValue("MainMenuHomeLabel", value); }
        }

        public string MainMenuTripLabel
        {
            get { return (string)this.GetValue("MainMenuTripLabel"); }
            set { this.SetValue("MainMenuTripLabel", value); }
        }

        public string MainMenuAdvancesLabel
        {
            get { return (string)this.GetValue("MainMenuAdvancesLabel"); }
            set { this.SetValue("MainMenuAdvancesLabel", value); }
        }

        public string MainMenuDelayEmergencyLabel
        {
            get { return (string)this.GetValue("MainMenuDelayEmergencyLabel"); }
            set { this.SetValue("MainMenuDelayEmergencyLabel", value); }
        }

        public string MainMenuRouteLabel
        {
            get { return (string)this.GetValue("MainMenuRouteLabel"); }
            set { this.SetValue("MainMenuRouteLabel", value); }
        }

        public string MainMenuPointsAndRewardsLabel
        {
            get { return (string)this.GetValue("MainMenuPointsAndRewardsLabel"); }
            set { this.SetValue("MainMenuPointsAndRewardsLabel", value); }
        }

        public string MainMenuSettingsLabel
        {
            get { return (string)this.GetValue("MainMenuSettingsLabel"); }
            set { this.SetValue("MainMenuSettingsLabel", value); }
        }

        public string MainMenuHelpLabel
        {
            get { return (string)this.GetValue("MainMenuHelpLabel"); }
            set { this.SetValue("MainMenuHelpLabel", value); }
        }
        #endregion

        #region Home page
        public string HomeWaitingForTripLabel
        {
            get { return (string)this.GetValue("HomeWaitingFirTripLabel"); }
            set { this.SetValue("HomeWaitingFirTripLabel", value); }
        }

        public string HomeNextTripLabel
        {
            get { return (string)this.GetValue("HomeNextTripLabel"); }
            set { this.SetValue("HomeNextTripLabel", value); }
        }

        public string HomeOriginLabel
        {
            get { return (string)this.GetValue("HomeOriginLabel"); }
            set { this.SetValue("HomeOriginLabel", value); }
        }

        public string HomeDestinationLabel
        {
            get { return (string)this.GetValue("HomeDistinationLabel"); }
            set { this.SetValue("HomeDistinationLabel", value); }
        }

        public string HomePointsLabel
        {
            get { return (string)this.GetValue("HomePointsLabel"); }
            set { this.SetValue("HomePointsLabel", value); }
        }

        public string HomeDeclinedLabel
        {
            get { return (string)this.GetValue("HomeDeclinedLabel"); }
            set { this.SetValue("HomeDeclinedLabel", value); }
        }

        public string HomeCancelledTripLabel
        {
            get { return (string)this.GetValue("HomeCancelledTripLabel"); }
            set { this.SetValue("HomeCancelledTripLabel", value); }
        }

        public string HomeArrivedOnTimeLabel
        {
            get { return (string)this.GetValue("HomeArrivedOnTimeLabel"); }
            set { this.SetValue("HomeArrivedOnTimeLabel", value); }
        }

        public string HomeArrivedOnTimeBonusPointsLabel
        {
            get { return (string)this.GetValue("HomeArrivedOnTimeBonusPointsLabel"); }
            set { this.SetValue("HomeArrivedOnTimeBonusPointsLabel", value); }
        }

        public string HomeArrivedOnTimeBonusPointsMinsLabel
        {
            get { return (string)this.GetValue("HomeArrivedOnTimeBonusPointsMinsLabel"); }
            set { this.SetValue("HomeArrivedOnTimeBonusPointsMinsLabel", value); }
        }

        public string HomeTotalPointsLabel
        {
            get { return (string)this.GetValue("HomeTotalPointsLabel"); }
            set { this.SetValue("HomeTotalPointsLabel", value); }
        }

        public string HomeBonusPointsForPhotoLabel
        {
            get { return (string)this.GetValue("HomeBonusPointsForPhotoLabel"); }
            set { this.SetValue("HomeBonusPointsForPhotoLabel", value); }
        }

        public string HomeBonusPointsForTimeLabel
        {
            get { return (string)this.GetValue("HomeBonusPointsForTimeLabel"); }
            set { this.SetValue("HomeBonusPointsForTimeLabel", value); }
        }

        public string HomeArrivedLateLabel
        {
            get { return (string)this.GetValue("HomeArrivedLateLabel"); }
            set { this.SetValue("HomeArrivedLateLabel", value); }
        }

        public string HomeArrivedLateBonusLabel
        {
            get { return (string)this.GetValue("HomeArrivedLateBonusLabel"); }
            set { this.SetValue("HomeArrivedLateBonusLabel", value); }
        }

        public string HomeDeclinedReason_1
        {
            get { return (string)this.GetValue("HomeDeclinedReason_1"); }
            set { this.SetValue("HomeDeclinedReason_1", value); }
        }

        public string HomeDeclinedReason_2
        {
            get { return (string)this.GetValue("HomeDeclinedReason_2"); }
            set { this.SetValue("HomeDeclinedReason_2", value); }
        }

        public string HomeDeclinedOtherReason
        {
            get { return (string)this.GetValue("HomeDeclinedOtherReason"); }
            set { this.SetValue("HomeDeclinedOtherReason", value); }
        }

        public string HomeDeclinedOtherReasonPlaceholder
        {
            get { return (string)this.GetValue("HomeDeclinedOtherReasonPlaceholder"); }
            set { this.SetValue("HomeDeclinedOtherReasonPlaceholder", value); }
        }

        public string HomeDeclineButtonText
        {
            get { return (string)this.GetValue("HomeDeclineButtonText"); }
            set { this.SetValue("HomeDeclineButtonText", value); }
        }

        public string HomeAcceptButtonText
        {
            get { return (string)this.GetValue("HomeAcceptButtonText"); }
            set { this.SetValue("HomeAcceptButtonText", value); }
        }

        public string HomeSubmitButtonText
        {
            get { return (string)this.GetValue("HomeSubmitButtonText"); }
            set { this.SetValue("HomeSubmitButtonText", value); }
        }

        public string HomeContinueButtonText
        {
            get { return (string)this.GetValue("HomeContinueButtonText"); }
            set { this.SetValue("HomeContinueButtonText", value); }
        }

        public string HomeDeclinedSubmitErrorText
        {
            get { return (string)this.GetValue("HomeDeclinedSubmitErrorText"); }
            set { this.SetValue("HomeDeclinedSubmitErrorText", value); }
        }

        public string HomeGPSPopupMainLabel
        {
            get { return (string)this.GetValue("HomeGPSPopupMainLabel"); }
            set { this.SetValue("HomeGPSPopupMainLabel", value); }
        }

        public string HomeGPSPopupSmallerLabel
        {
            get { return (string)this.GetValue("HomeGPSPopupSmallerLabel"); }
            set { this.SetValue("HomeGPSPopupSmallerLabel", value); }
        }

        public string HomeGPSPopupCancelButtonText
        {
            get { return (string)this.GetValue("HomeGPSPopupCancelButtonText"); }
            set { this.SetValue("HomeGPSPopupCancelButtonText", value); }
        }

        public string HomeGPSPopupSettingsButtonText
        {
            get { return (string)this.GetValue("HomeGPSPopupSettingsButtonText"); }
            set { this.SetValue("HomeGPSPopupSettingsButtonText", value); }
        }

		public string HomeCongratulations
		{
			get { return (string)this.GetValue ("HomeCongratulations"); }
			set { this.SetValue ("HomeCongratulations", value); }
		}

		public string HomeRewardsButtonText
		{
			get { return (string)this.GetValue ("HomeRewardsButtonText"); }
			set { this.SetValue ("HomeRewardsButtonText", value); }
		}
        #endregion

        #region Trip page
        public string TripShipperTitleLabel
        {
            get { return (string)this.GetValue("TripShipperTitleLabel"); }
            set { this.SetValue("TripShipperTitleLabel", value); }
        }

        public string TripReceiverTitleLabel
        {
            get { return (string)this.GetValue("TripReceiverTitleLabel"); }
            set { this.SetValue("TripReceiverTitleLabel", value); }
        }
        #endregion

        #region Contractor page
        public string ContractorPageNameLabel
        {
            get { return (string)this.GetValue("ContractorPageNameLabel"); }
            set { this.SetValue("ContractorPageNameLabel", value); }
        }

        public string ContractorPagePhoneLabel
        {
            get { return (string)this.GetValue("ContractorPagePhoneLabel"); }
            set { this.SetValue("ContractorPagePhoneLabel", value); }
        }

        public string ContractorPageFaxLabel
        {
            get { return (string)this.GetValue("ContractorPageFaxLabel"); }
            set { this.SetValue("ContractorPageFaxLabel", value); }
        }

        public string ContractorPageAddressLabel
        {
            get { return (string)this.GetValue("ContractPageAddressLabel"); }
            set { this.SetValue("ContractPageAddressLabel", value); }
        }
        #endregion

        #region Fuel Advance Page
        public string FuelAdvanceNoneLabel
        {
            get { return (string)this.GetValue("FuelAdvanceNoneLabel"); }
            set { this.SetValue("FuelAdvanceNoneLabel", value); }
        }

        public string FuelAdvanceRequestedLabel
        {
            get { return (string)this.GetValue("FuelAdvanceRequestedLabel"); }
            set { this.SetValue("FuelAdvanceRequestedLabel", value); }
        }

        public string FuelAdvanceReceivedLabel
        {
            get { return (string)this.GetValue("FuelAdvanceReceivedLabel"); }
            set { this.SetValue("FuelAdvanceReceivedLabel", value); }
        }

        public string FuelAdvanceReceivedInfoLabel
        {
            get { return (string)this.GetValue("FuelAdvanceReceivedInfoLabel"); }
            set { this.SetValue("FuelAdvanceReceivedInfoLabel", value); }
        }

        public string FuelAdvanceCompletedLabel
        {
            get { return (string)this.GetValue("FuelAdvanceCompletedLabel"); }
            set { this.SetValue("FuelAdvanceCompletedLabel", value); }
        }

        public string FuelAdvanceNoneRequestButtonText
        {
            get { return (string)this.GetValue("FuelAdvanceNoneRequestButtonText"); }
            set { this.SetValue("FuelAdvanceNoneRequestButtonText", value); }
        }

        public string FuelAdvanceReceivedResendButtonText
        {
            get { return (string)this.GetValue("FuelAdvanceReceivedResendButtonText"); }
            set { this.SetValue("FuelAdvanceReceivedResendButtonText", value); }
        }

        public string FuelAdvanceReceivedCancelButtonText
        {
            get { return (string)this.GetValue("FuelAdvanceReceivedCancelButtonText"); }
            set { this.SetValue("FuelAdvanceReceivedCancelButtonText", value); }
        }
        #endregion

        #region Lumper Page
        public string LumperNoneLabel
        {
            get { return (string)this.GetValue("LumperNoneLabel"); }
            set { this.SetValue("LumperNoneLabel", value); }
        }

        public string LumperRequestedLabel
        {
            get { return (string)this.GetValue("LumperRequestedLabel"); }
            set { this.SetValue("LumperRequestedLabel", value); }
        }

        public string LumperReceivedLabel
        {
            get { return (string)this.GetValue("LumperReceivedLabel"); }
            set { this.SetValue("LumperReceivedLabel", value); }
        }

        public string LumperReceivedInfoLabel
        {
            get { return (string)this.GetValue("LumperReceivedInfoLabel"); }
            set { this.SetValue("LumperReceivedInfoLabel", value); }
        }

        public string LumperCompletedLabel
        {
            get { return (string)this.GetValue("LumperCompletedLabel"); }
            set { this.SetValue("LumperCompletedLabel", value); }
        }

        public string LumperNoneRequestButtonText
        {
            get { return (string)this.GetValue("LumperNoneRequestButtonText"); }
            set { this.SetValue("LumperNoneRequestButtonText", value); }
        }

        public string LumperReceivedResendButtonText
        {
            get { return (string)this.GetValue("LumperReceivedResendButtonText"); }
            set { this.SetValue("LumperReceivedResendButtonText", value); }
        }

        public string LumperReceivedCancelButtonText
        {
            get { return (string)this.GetValue("LumperReceivedCancelButtonText"); }
            set { this.SetValue("LumperReceivedCancelButtonText", value); }
        }
        #endregion

        #region Delay / Emergency page
        public string DelaySelectTypeLabel
        {
            get { return (string)this.GetValue("DelaySelectTypeLabel"); }
            set { this.SetValue("DelaySelectTypeLabel", value); }
        }

        public string DelayFlatTireLabel
        {
            get { return (string)this.GetValue("DelayFlatTireLabel"); }
            set { this.SetValue("DelayFlatTireLabel", value); }
        }

        public string DelayFeelingSleepyLabel
        {
            get { return (string)this.GetValue("DelayFeelingSleepyLabel"); }
            set { this.SetValue("DelayFeelingSleepyLabel", value); }
        }

        public string DelayRoadWorkAheadLabel
        {
            get { return (string)this.GetValue("DelayRoadWorkAheadLabel"); }
            set { this.SetValue("DelayRoadWorkAheadLabel", value); }
        }

        public string DelayCommentsPlaceholderText
        {
            get { return (string)this.GetValue("DelayCommentsPlaceholderText"); }
            set { this.SetValue("DelayCommentsPlaceholderText", value); }
        }

        public string DelaySubmitButtonText
        {
            get { return (string)this.GetValue("DelaySubmitButtonText"); }
            set { this.SetValue("DelaySubmitButtonText", value); }
        }
        #endregion

        #region Months
        public string January
        {
            get { return (string)this.GetValue("January"); }
            set { this.SetValue("January", value); }
        }

        public string February
        {
            get { return (string)this.GetValue("February"); }
            set { this.SetValue("February", value); }
        }

        public string March
        {
            get { return (string)this.GetValue("March"); }
            set { this.SetValue("March", value); }
        }

        public string April
        {
            get { return (string)this.GetValue("April"); }
            set { this.SetValue("April", value); }
        }

        public string May
        {
            get { return (string)this.GetValue("May"); }
            set { this.SetValue("May", value); }
        }

        public string June
        {
            get { return (string)this.GetValue("June"); }
            set { this.SetValue("June", value); }
        }

        public string July
        {
            get { return (string)this.GetValue("July"); }
            set { this.SetValue("July", value); }
        }

        public string August
        {
            get { return (string)this.GetValue("August"); }
            set { this.SetValue("August", value); }
        }

        public string September
        {
            get { return (string)this.GetValue("September"); }
            set { this.SetValue("September", value); }
        }

        public string October
        {
            get { return (string)this.GetValue("October"); }
            set { this.SetValue("October", value); }
        }

        public string November
        {
            get { return (string)this.GetValue("November"); }
            set { this.SetValue("November", value); }
        }

        public string December
        {
            get { return (string)this.GetValue("December"); }
            set { this.SetValue("December", value); }
        }
        #endregion

        #region Times
        public string TimeAM
        {
            get { return (string)this.GetValue("TimeAM"); }
            set { this.SetValue("TimeAM", value); }
        }

        public string TimePM
        {
            get { return (string)this.GetValue("TimePM"); }
            set { this.SetValue("TimePM", value); }
        }
        #endregion

		#region Sign up
		public string SignUpLabel
		{
			get { return (string)this.GetValue ("SignUp"); }
			set { this.SetValue ("SignUp", value); }
		}
        #endregion

        #region Driver authorization page
        public string DriverAuthorizationCommonLabel
        {
            get { return (string)this.GetValue("DriverAuthorizationCommonLabel"); }
            set { this.SetValue("DriverAuthorizationCommonLabel", value); }
        }

        public string DriverAuthorizationAssignIDNumberPlaceholder
        {
            get { return (string)this.GetValue("DriverAuthorizationAssignIDNumberPlaceholder"); }
            set { this.SetValue("DriverAuthorizationAssignIDNumberPlaceholder", value); }
        }

        public string DriverAuthorizationAuthorizeButtonText
        {
            get { return (string)this.GetValue("DriverAuthorizationAuthorizeButtonText"); }
            set { this.SetValue("DriverAuthorizationAuthorizeButtonText", value); }
        }

        public string DriverAuthorizationDeclineButtonText
        {
            get { return (string)this.GetValue("DriverAuthorizationDeclineButtonText"); }
            set { this.SetValue("DriverAuthorizationDeclineButtonText", value); }
        }
        #endregion

        #region SignUpOwnerWelcome page
        public string SignUpOwnerWelcomeLabel
        {
            get { return (string)this.GetValue("SignUpOwnerWelcomeLabel"); }
            set { this.SetValue("SignUpOwnerWelcomeLabel", value); }
        }

        public string SignUpOwnerWelcomeContinueButtonText
        {
            get { return (string)this.GetValue("SignUpOwnerWelcomeContinueButtonText"); }
            set { this.SetValue("SignUpOwnerWelcomeContinueButtonText", value); }
        }
        #endregion

        #region SignUp user roles
        public string SignUpUserRoleOwnerOperator
        {
            get { return (string)this.GetValue("SignUpUserRoleOwnerOperator"); }
            set { this.SetValue("SignUpUserRoleOwnerOperator", value); }
        }
        #endregion
    }
    #endregion
}
