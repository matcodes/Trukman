using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowOwnerFleetPageMessage
    public class ShowOwnerFleetPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowOwnerFleetPageMessage";

        public static void Send()
        {
            var message = new ShowOwnerFleetPageMessage();
            MessagingCenter.Send<ShowOwnerFleetPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowOwnerFleetPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowOwnerFleetPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowOwnerFleetPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowOwnerFleetPageMessage()
        {
        }
    }
    #endregion
}
