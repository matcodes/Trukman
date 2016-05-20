using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowAdvancesPageMessage
    public class ShowAdvancesPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowAdvancesPageMessage";

		public static void Send(Trip trip)
        {
			var message = new ShowAdvancesPageMessage(trip);
            MessagingCenter.Send<ShowAdvancesPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowAdvancesPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowAdvancesPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowAdvancesPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

		public ShowAdvancesPageMessage(Trip trip)
        {
			this.Trip = trip;
        }

		public Trip Trip { get; private set; }
    }
    #endregion
}
