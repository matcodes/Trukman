using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region HideMainMenuMessage
    public class HideMainMenuMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "HideMainMenuMessage";

        public static void Send()
        {
            var message = new HideMainMenuMessage();
            MessagingCenter.Send<HideMainMenuMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<HideMainMenuMessage> action)
        {
            MessagingCenter.Subscribe<HideMainMenuMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<HideMainMenuMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public HideMainMenuMessage()
        {
        }
    }
    #endregion
}
