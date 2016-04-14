using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowMainMenuMessage
    public class ShowMainMenuMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowMainMenuMessage";

        public static void Send()
        {
            var message = new ShowMainMenuMessage();
            MessagingCenter.Send<ShowMainMenuMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowMainMenuMessage> action)
        {
            MessagingCenter.Subscribe<ShowMainMenuMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowMainMenuMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowMainMenuMessage()
        {
        }
    }
    #endregion
}
