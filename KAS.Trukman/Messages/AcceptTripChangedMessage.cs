using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region AcceptTripChangedMessage
    public class AcceptTripChangedMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "AcceptTripChangedMessage";

        public static void Send()
        {
            var message = new AcceptTripChangedMessage();
            MessagingCenter.Send<AcceptTripChangedMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<AcceptTripChangedMessage> action)
        {
            MessagingCenter.Subscribe<AcceptTripChangedMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<AcceptTripChangedMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public AcceptTripChangedMessage()
        {
        }
    }
    #endregion
}
