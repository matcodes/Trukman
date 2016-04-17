using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowHelpPageMessage
    public class ShowHelpPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowHelpPageMessage";

        public static void Send()
        {
            var message = new ShowHelpPageMessage();
            MessagingCenter.Send<ShowHelpPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowHelpPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowHelpPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowHelpPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowHelpPageMessage()
        {
        }
    }
    #endregion
}
