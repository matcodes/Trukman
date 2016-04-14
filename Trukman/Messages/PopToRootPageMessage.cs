using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Trukman.Messages
{
    #region PopToRootMessage
    public class PopToRootPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "PopToRootPageMessage";

        public static void Send()
        {
            PopToRootPageMessage message = new PopToRootPageMessage();
            MessagingCenter.Send<PopToRootPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<PopToRootPageMessage> action)
        {
            MessagingCenter.Subscribe<PopToRootPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<PopToRootPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public PopToRootPageMessage()
        {
        }
    }
    #endregion
}
