using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowOwnerInvoiceListPageMessage
    public class ShowOwnerInvoiceListPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowOwnerInvoiceListPageMessage";

        public static void Send()
        {
            var message = new ShowOwnerInvoiceListPageMessage();
            MessagingCenter.Send<ShowOwnerInvoiceListPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowOwnerInvoiceListPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowOwnerInvoiceListPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowOwnerInvoiceListPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowOwnerInvoiceListPageMessage()
        {
        }
    }
    #endregion
}
