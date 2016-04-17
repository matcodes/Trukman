using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using KAS.Trukman.Data.Interfaces;

namespace KAS.Trukman.Messages
{
    #region ShowAdvancesPageMessage
    public class ShowAdvancesPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowAdvancesPageMessage";

		public static void Send(ITrip trip)
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

		public ShowAdvancesPageMessage(ITrip trip)
        {
			this.Trip = trip;
        }

		public ITrip Trip { get; private set; }
    }
    #endregion
}
