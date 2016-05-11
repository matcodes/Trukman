using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowNotificationMessage
    public class ShowNotificationMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowNotificationMessage";

        public static void Send(string messageText)
        {
            var message = new ShowNotificationMessage(messageText);
            MessagingCenter.Send<ShowNotificationMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowNotificationMessage> action)
        {
            MessagingCenter.Subscribe<ShowNotificationMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowNotificationMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowNotificationMessage(string messageText)
            : base()
        {
            this.MessageText = messageText;
        }

        public string MessageText { get; private set; }
    }
    #endregion
}
