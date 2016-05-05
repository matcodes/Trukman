using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region DeclineTripSubmitMessage
    public class DeclineTripSubmitMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "DeclineTripSubmitMessage";

        public static void Send(string reasonText)
        {
            var message = new DeclineTripSubmitMessage(reasonText);
            MessagingCenter.Send<DeclineTripSubmitMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<DeclineTripSubmitMessage> action)
        {
            MessagingCenter.Subscribe<DeclineTripSubmitMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<DeclineTripSubmitMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public DeclineTripSubmitMessage(string reasonText)
        {
            this.ReasonText = reasonText;
        }

        public string ReasonText { get; private set; }
    }
    #endregion
}
