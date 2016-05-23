using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowOwnerInvoiceViewerPageMessage
    public class ShowOwnerInvoiceViewerPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowOwnerInvoiceViewerPageMessage";

        public static void Send(string invoiceUri)
        {
            var message = new ShowOwnerInvoiceViewerPageMessage(invoiceUri);
            MessagingCenter.Send<ShowOwnerInvoiceViewerPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowOwnerInvoiceViewerPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowOwnerInvoiceViewerPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowOwnerInvoiceViewerPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowOwnerInvoiceViewerPageMessage(string invoiceUri)
        {
            this.InvoiceUri = invoiceUri;
        }

        public string InvoiceUri { get; private set; }
    }
    #endregion
}
