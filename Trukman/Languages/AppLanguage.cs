using Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trukman.Languages
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

                this.ShipperInfoPageName = appLanguage.ShipperInfoPageName;
                this.ReceiverInfoPageName = appLanguage.ReceiverInfoPageName;
                this.FuelAdvancePageName = appLanguage.FuelAdvancePageName;

                this.ContractorPageNameLabel = appLanguage.ContractorPageNameLabel;
                this.ContractorPagePhoneLabel = appLanguage.ContractorPagePhoneLabel;
                this.ContractorPageFaxLabel = appLanguage.ContractorPageFaxLabel;
                this.ContractorPageAddressLabel = appLanguage.ContractorPageAddressLabel;

                this.FuelAdvanceNoneLabel = appLanguage.FuelAdvanceNoneLabel;
                this.FuelAdvanceRequestedLabel = appLanguage.FuelAdvanceRequestedLabel;
                this.FuelAdvanceReceivedLabel = appLanguage.FuelAdvanceReceivedLabel;
                this.FuelAdvanceReceivedInfoLabel = appLanguage.FuelAdvanceReceivedInfoLabel;
                this.FuelAdvanceCompletedLabel = appLanguage.FuelAdvanceCompletedLabel;

                this.FuelAdvanceNoneRequestButtonText = appLanguage.FuelAdvanceNoneRequestButtonText;
                this.FuelAdvanceReceivedResendButtonText = appLanguage.FuelAdvanceReceivedResendButtonText;
                this.FuelAdvanceReceivedCancelButtonText = appLanguage.FuelAdvanceReceivedCancelButtonText;
				this.TrukmanLabel = appLanguage.TrukmanLabel;

				this.DriverListLabel = appLanguage.DriverListLabel;

				this.SignUpLabel = appLanguage.SignUpLabel;
            }
        }

        public string DisplayName { get; private set; }

        public string AppName
        {
            get { return (string)this.GetValue("AppName"); }
            set { this.SetValue("AppName", value); }
        }

        #region Page names
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

        public string FuelAdvancePageName
        {
            get { return (string)this.GetValue("FuelAdvancePageName"); }
            set { this.SetValue("FuelAdvancePageName", value); }
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

		#region Home Page
		public string TrukmanLabel
		{
			get { return (string)this.GetValue ("TrukmanLabel"); }
			set { this.SetValue ("TrukmanLabel", value); }
		}
		#endregion

		#region Driver list page
		public string DriverListLabel
		{
			get { return (string)this.GetValue ("DriverListLabel"); }
			set { this.SetValue ("DriverListLabel", value); }
		}
		#endregion

		#region Sign up
		public string SignUpLabel
		{
			get { return (string)this.GetValue ("SignUp"); }
			set { this.SetValue ("SignUp", value); }
		}
		#endregion
    }
    #endregion
}
