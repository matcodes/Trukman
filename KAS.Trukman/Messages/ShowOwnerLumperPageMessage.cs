using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowOwnerLumperPageMessage
    public class ShowOwnerLumperPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowOwnerLumperPageMessage";

        public static void Send()
        {
            var message = new ShowOwnerLumperPageMessage();
            MessagingCenter.Send<ShowOwnerLumperPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowOwnerLumperPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowOwnerLumperPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowOwnerLumperPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowOwnerLumperPageMessage()
        {
        }
    }
    #endregion
}
