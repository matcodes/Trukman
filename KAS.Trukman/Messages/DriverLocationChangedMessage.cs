using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region DriverLocationChangedMessage
    public class DriverLocationChangedMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "DriverLocationChangedMessage";

        public static void Send()
        {
            var message = new DriverLocationChangedMessage();
            MessagingCenter.Send<DriverLocationChangedMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<DriverLocationChangedMessage> action)
        {
            MessagingCenter.Subscribe<DriverLocationChangedMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<DriverLocationChangedMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public DriverLocationChangedMessage()
        {
        }
    }
    #endregion
}
