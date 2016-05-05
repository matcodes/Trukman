using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region TripShipperPositionChangedMessage
    public class TripShipperPositionChangedMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "TripShipperPositionChangedMessage";

        public static void Send()
        {
            var message = new TripShipperPositionChangedMessage();
            MessagingCenter.Send<TripShipperPositionChangedMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<TripShipperPositionChangedMessage> action)
        {
            MessagingCenter.Subscribe<TripShipperPositionChangedMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<TripShipperPositionChangedMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public TripShipperPositionChangedMessage()
        {
        }
    }
    #endregion
}
