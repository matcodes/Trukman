using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowOwnerUserAuthorizationPageMessage
    public class ShowOwnerUserAuthorizationPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowOwnerUserAuthorizationPageMessage";

        public static void Send(string companyName, User driver, string companyID)
        {
            var message = new ShowOwnerUserAuthorizationPageMessage(companyName, driver, companyID);
            MessagingCenter.Send<ShowOwnerUserAuthorizationPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowOwnerUserAuthorizationPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowOwnerUserAuthorizationPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowOwnerUserAuthorizationPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowOwnerUserAuthorizationPageMessage(string companyName, User driver, string companyID)
        {
            this.CompanyName = companyName;
            this.Driver = driver;
            this.CompanyID = companyID;
        }

        public string CompanyName { get; private set; }

        public User Driver { get; private set; }

        public string CompanyID { get; private set; }
    }
    #endregion
}
