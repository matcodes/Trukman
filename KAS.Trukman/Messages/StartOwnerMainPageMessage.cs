using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region StartOwnerMainPageMessage
    public class StartOwnerMainPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "StartOwnerMainPageMessage";

        public static void Send()
        {
            var message = new StartOwnerMainPageMessage();
            MessagingCenter.Send<StartOwnerMainPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<StartOwnerMainPageMessage> action)
        {
            MessagingCenter.Subscribe<StartOwnerMainPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<StartOwnerMainPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public StartOwnerMainPageMessage()
        {
        }
    }
    #endregion
}
