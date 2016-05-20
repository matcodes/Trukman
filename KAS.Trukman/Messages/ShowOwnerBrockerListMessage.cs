using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowOwnerBrockerListMessage
    public class ShowOwnerBrockerListMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowOwnerBrockerListMessage";

        public static void Send()
        {
            var message = new ShowOwnerBrockerListMessage();
            MessagingCenter.Send<ShowOwnerBrockerListMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowOwnerBrockerListMessage> action)
        {
            MessagingCenter.Subscribe<ShowOwnerBrockerListMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowOwnerBrockerListMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowOwnerBrockerListMessage()
        {
        }
    }
    #endregion
}
