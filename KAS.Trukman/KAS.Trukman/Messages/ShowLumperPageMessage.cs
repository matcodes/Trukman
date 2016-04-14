using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowLumperPageMessage
    public class ShowLumperPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowLumperPageMessage";

        public static void Send()
        {
            var message = new ShowLumperPageMessage();
            MessagingCenter.Send<ShowLumperPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowLumperPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowLumperPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowLumperPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowLumperPageMessage()
        {
        }
    }
    #endregion
}
