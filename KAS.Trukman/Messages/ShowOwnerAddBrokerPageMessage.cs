using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    public class ShowOwnerAddBrokerPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowOwnerAddBrokerPageMessage";

        public static void Send()
        {
            var message = new ShowOwnerAddBrokerPageMessage();
            MessagingCenter.Send<ShowOwnerAddBrokerPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowOwnerAddBrokerPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowOwnerAddBrokerPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowOwnerAddBrokerPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowOwnerAddBrokerPageMessage()
        {
        }
    }
}

