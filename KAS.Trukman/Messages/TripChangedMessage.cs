using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region TripChangedMessage
    public class TripChangedMessage 
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "TripChangedMessage";

        public static void Send(Trip trip)
        {
            var message = new TripChangedMessage(trip);
            MessagingCenter.Send<TripChangedMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<TripChangedMessage> action)
        {
            MessagingCenter.Subscribe<TripChangedMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<TripChangedMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public TripChangedMessage(Trip trip)
        {
            this.Trip = trip;
        }

        public Trip Trip { get; private set; }
    }
    #endregion
}
