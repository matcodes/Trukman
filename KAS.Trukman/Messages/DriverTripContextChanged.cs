using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region DriverTripContextChangedMessage
    public class DriverTripContextChangedMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "DriverTripContextChangedMessage";

        public static void Send()
        {
            var message = new DriverTripContextChangedMessage();
            MessagingCenter.Send<DriverTripContextChangedMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<DriverTripContextChangedMessage> action)
        {
            MessagingCenter.Subscribe<DriverTripContextChangedMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<DriverTripContextChangedMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public DriverTripContextChangedMessage()
        {
        }
    }
    #endregion
}
