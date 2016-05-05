using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region CancelledTripChangedMessage
    public class CancelledTripChangedMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "CancelledTripChangedMessage";

        public static void Send()
        {
            var message = new CancelledTripChangedMessage();
            MessagingCenter.Send<CancelledTripChangedMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<CancelledTripChangedMessage> action)
        {
            MessagingCenter.Subscribe<CancelledTripChangedMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<CancelledTripChangedMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public CancelledTripChangedMessage()
        {
        }
    }
    #endregion
}
