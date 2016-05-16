using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowOwnerFuelAdvancePageMessage
    public class ShowOwnerFuelAdvancePageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowOwnerFuelAdvancePageMessage";

        public static void Send()
        {
            var message = new ShowOwnerFuelAdvancePageMessage();
            MessagingCenter.Send<ShowOwnerFuelAdvancePageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowOwnerFuelAdvancePageMessage> action)
        {
            MessagingCenter.Subscribe<ShowOwnerFuelAdvancePageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowOwnerFuelAdvancePageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowOwnerFuelAdvancePageMessage()
        {
        }
    }
    #endregion
}
