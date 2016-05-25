using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowOwnerDelayAlertsPageMessage
    public class ShowOwnerDelayAlertsPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowOwnerDelayAlertsPageMessage";

        public static void Send()
        {
            var message = new ShowOwnerDelayAlertsPageMessage();
            MessagingCenter.Send<ShowOwnerDelayAlertsPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowOwnerDelayAlertsPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowOwnerDelayAlertsPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowOwnerDelayAlertsPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowOwnerDelayAlertsPageMessage()
        {
        }
    }
    #endregion
}
