using KAS.Trukman.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Trukman.Interfaces;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowDriverAuthorizePageMessage
    public class ShowDriverAuthorizePageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowDriverAuthorizePageMessage";

        public static void Send(IUser driver)
        {
            var message = new ShowDriverAuthorizePageMessage(driver);
            MessagingCenter.Send<ShowDriverAuthorizePageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowDriverAuthorizePageMessage> action)
        {
            MessagingCenter.Subscribe<ShowDriverAuthorizePageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowDriverAuthorizePageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowDriverAuthorizePageMessage(IUser driver)
        {
            this.Driver = driver;
        }

        public IUser Driver { get; private set; }
    }
    #endregion
}
