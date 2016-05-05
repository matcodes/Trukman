using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region TripReceiverPositionChangedMessage
    public class TripReceiverPositionChangedMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "TripReceiverPositionChangedMessage";

        public static void Send()
        {
            var message = new TripReceiverPositionChangedMessage();
            MessagingCenter.Send<TripReceiverPositionChangedMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<TripReceiverPositionChangedMessage> action)
        {
            MessagingCenter.Subscribe<TripReceiverPositionChangedMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<TripReceiverPositionChangedMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public TripReceiverPositionChangedMessage()
        {
        }
    }
    #endregion
}
