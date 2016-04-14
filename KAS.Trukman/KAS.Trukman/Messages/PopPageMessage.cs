using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region PopPageMessage
    public class PopPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "PopPageMessage";

        public static void Send()
        {
            var message = new PopPageMessage();
            MessagingCenter.Send<PopPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<PopPageMessage> action)
        {
            MessagingCenter.Subscribe<PopPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<PopPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public PopPageMessage()
        {
        }
    }
    #endregion
}
