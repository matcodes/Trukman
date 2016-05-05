using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region CompletedTripChangedMessage
    public class CompletedTripChangedMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "CompletedTripChangedMessage";

        public static void Send()
        {
            var message = new CompletedTripChangedMessage();
            MessagingCenter.Send<CompletedTripChangedMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<CompletedTripChangedMessage> action)
        {
            MessagingCenter.Subscribe<CompletedTripChangedMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<CompletedTripChangedMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public CompletedTripChangedMessage()
        {
        }
    }
    #endregion
}
