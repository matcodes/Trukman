using KAS.Trukman.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowRoutePageMessage
    public class ShowRoutePageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowRoutePageMessage";

        public static void Send(ITrip trip)
        {
            var message = new ShowRoutePageMessage(trip);
            MessagingCenter.Send<ShowRoutePageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowRoutePageMessage> action)
        {
            MessagingCenter.Subscribe<ShowRoutePageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowRoutePageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowRoutePageMessage(ITrip trip)
        {
            this.Trip = trip;
        }

        public ITrip Trip { get; private set; }
    }
    #endregion
}
