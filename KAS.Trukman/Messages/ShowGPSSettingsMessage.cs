using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowGPSSettingsMessage
    public class ShowGPSSettingsMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowGPSSettingsMessage";

        public static void Send()
        {
            var message = new ShowGPSSettingsMessage();
            MessagingCenter.Send<ShowGPSSettingsMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowGPSSettingsMessage> action)
        {
            MessagingCenter.Subscribe<ShowGPSSettingsMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowGPSSettingsMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowGPSSettingsMessage()
        {
        }
    }
    #endregion
}
