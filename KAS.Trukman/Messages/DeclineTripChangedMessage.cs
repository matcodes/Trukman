using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region DeclineTripChangedMessage
    public class DeclineTripChangedMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "DeclineTripChangedMessage";

        public static void Send()
        {
            var message = new DeclineTripChangedMessage();
            MessagingCenter.Send<DeclineTripChangedMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<DeclineTripChangedMessage> action)
        {
            MessagingCenter.Subscribe<DeclineTripChangedMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<DeclineTripChangedMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public DeclineTripChangedMessage()
            : base()
        {
        }
    }
    #endregion
}
