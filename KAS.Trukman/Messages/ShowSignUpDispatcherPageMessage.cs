using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowSignUpDispatcherPageMessage
    public class ShowSignUpDispatcherPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowSignUpDispatcherPageMessage";

        public static void Send()
        {
            var message = new ShowSignUpDispatcherPageMessage();
            MessagingCenter.Send<ShowSignUpDispatcherPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowSignUpDispatcherPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowSignUpDispatcherPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowSignUpDispatcherPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowSignUpDispatcherPageMessage()
        {
        }
    }
    #endregion
}
