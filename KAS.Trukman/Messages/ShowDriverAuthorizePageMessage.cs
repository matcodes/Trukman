using KAS.Trukman.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Trukman.Interfaces;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowDriverAuthorizationPageMessage
    public class ShowDriverAuthorizationPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowDriverAuthorizationPageMessage";

        public static void Send(IUser driver)
        {
            var message = new ShowDriverAuthorizationPageMessage(driver);
            MessagingCenter.Send<ShowDriverAuthorizationPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowDriverAuthorizationPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowDriverAuthorizationPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowDriverAuthorizationPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowDriverAuthorizationPageMessage(IUser driver)
        {
            this.Driver = driver;
        }

        public IUser Driver { get; private set; }
    }
    #endregion
}
