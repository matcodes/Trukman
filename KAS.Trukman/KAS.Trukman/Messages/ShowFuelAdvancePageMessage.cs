using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowFuelAdvancePageMessage
    public class ShowFuelAdvancePageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowFuelAdvancePageMessage";

        public static void Send()
        {
            var message = new ShowFuelAdvancePageMessage();
            MessagingCenter.Send<ShowFuelAdvancePageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowFuelAdvancePageMessage> action)
        {
            MessagingCenter.Subscribe<ShowFuelAdvancePageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowFuelAdvancePageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowFuelAdvancePageMessage()
        {
        }
    }
    #endregion
}
