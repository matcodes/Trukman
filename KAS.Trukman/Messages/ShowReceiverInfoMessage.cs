using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowReceiverInfoPageMessage
    public class ShowReceiverInfoPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowReceiverInfoPageMessage";

        public static void Send(Contractor receiver)
        {
            var message = new ShowReceiverInfoPageMessage(receiver);
            MessagingCenter.Send<ShowReceiverInfoPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowReceiverInfoPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowReceiverInfoPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowReceiverInfoPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowReceiverInfoPageMessage(Contractor receiver)
            : base()
        {
            this.Receiver = receiver;
        }

        public Contractor Receiver { get; private set; }
    }
    #endregion
}
