using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowSignUpDriverPageMessage
    public class ShowSignUpDriverPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowSignUpDriverPageMessage";

        public static void Send()
        {
            var message = new ShowSignUpDriverPageMessage();
            MessagingCenter.Send<ShowSignUpDriverPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowSignUpDriverPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowSignUpDriverPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowSignUpDriverPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowSignUpDriverPageMessage()
        {
        }
    }
    #endregion
}
