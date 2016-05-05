using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowSignUpOwnerMCPageMessage
    public class ShowSignUpOwnerMCPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowSignUpOwnerMCPageMessage";

        public static void Send()
        {
            var message = new ShowSignUpOwnerMCPageMessage();
            MessagingCenter.Send<ShowSignUpOwnerMCPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowSignUpOwnerMCPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowSignUpOwnerMCPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowSignUpOwnerMCPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowSignUpOwnerMCPageMessage()
        {
        }
    }
    #endregion
}
