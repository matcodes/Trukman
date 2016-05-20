using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowTripPageMessage
    public class ShowTripPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowReceiverInfoPageMessage";

        public static void Send(Trip trip)
        {
            var message = new ShowTripPageMessage(trip);
            MessagingCenter.Send<ShowTripPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowTripPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowTripPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowTripPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowTripPageMessage(Trip trip)
        {
            this.Trip = trip;
        }

        public Trip Trip { get; private set; }
    }
    #endregion
}
