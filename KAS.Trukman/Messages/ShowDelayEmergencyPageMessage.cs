using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowDelayEmergencyPageMessage
    public class ShowDelayEmergencyPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowDelayEmergencyPageMessage";

		public static void Send(Trip trip)
        {
			var message = new ShowDelayEmergencyPageMessage(trip);
            MessagingCenter.Send<ShowDelayEmergencyPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowDelayEmergencyPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowDelayEmergencyPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowDelayEmergencyPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowDelayEmergencyPageMessage(Trip trip)
        {
			Trip = trip;
        }

		public Trip Trip { get; private set; }
    }
    #endregion
}
