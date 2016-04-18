using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowSettingsPageMessage
    public class ShowSettingsPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowSettingsPageMessage";

        public static void Send()
        {
            var message = new ShowSettingsPageMessage();
            MessagingCenter.Send<ShowSettingsPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowSettingsPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowSettingsPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowSettingsPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowSettingsPageMessage()
        {
        }
    }
    #endregion
}
