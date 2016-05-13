using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region MainPageInitializedMessage
    public class MainPageInitializedMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "MainPageInitializedMessage";

        public static void Send()
        {
            var message = new MainPageInitializedMessage();
            MessagingCenter.Send<MainPageInitializedMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<MainPageInitializedMessage> action)
        {
            MessagingCenter.Subscribe<MainPageInitializedMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<MainPageInitializedMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public MainPageInitializedMessage()
        {
        }
    }
    #endregion
}
