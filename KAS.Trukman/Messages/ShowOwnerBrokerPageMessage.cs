using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    public class ShowOwnerBrokerPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowOwnerBrokerPageMessage";

        public static void Send(BrokerUser brokerUser)
        {
            var message = new ShowOwnerBrokerPageMessage(brokerUser);
            MessagingCenter.Send<ShowOwnerBrokerPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowOwnerBrokerPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowOwnerBrokerPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowOwnerBrokerPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowOwnerBrokerPageMessage(BrokerUser brokerUser)
        {
            this.BrokerUser = brokerUser;
        }

        public BrokerUser BrokerUser { get; private set; }
    }
}

