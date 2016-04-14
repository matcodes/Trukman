using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region LanguageSelectedMessage
    public class LanguageSelectedMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "LanguageSelectedMessage";

        public static void Send()
        {
            var message = new LanguageSelectedMessage();
            MessagingCenter.Send<LanguageSelectedMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<LanguageSelectedMessage> action)
        {
            MessagingCenter.Subscribe<LanguageSelectedMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<LanguageSelectedMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public LanguageSelectedMessage()
        {
        }
    }
    #endregion
}
