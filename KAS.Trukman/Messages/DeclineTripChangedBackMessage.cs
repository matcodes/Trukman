using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region DeclineTripChangedBackMessage
    public class DeclineTripChangedBackMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "DeclineTripChangedBackMessage";

        public static void Send()
        {
            var message = new DeclineTripChangedBackMessage();
            MessagingCenter.Send<DeclineTripChangedBackMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<DeclineTripChangedBackMessage> action)
        {
            MessagingCenter.Subscribe<DeclineTripChangedBackMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<DeclineTripChangedBackMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public DeclineTripChangedBackMessage()
        {
        }
    }
    #endregion
}
