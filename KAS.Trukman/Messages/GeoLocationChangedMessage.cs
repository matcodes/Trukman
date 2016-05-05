using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region GeoLocationChangedMessage
    public class GeoLocationChangedMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "GeoLocationChangedMessage";

        public static void Send(double latitude, double longitude)
        {
            var message = new GeoLocationChangedMessage(latitude, longitude);
            MessagingCenter.Send<GeoLocationChangedMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<GeoLocationChangedMessage> action)
        {
            MessagingCenter.Subscribe<GeoLocationChangedMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<GeoLocationChangedMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public GeoLocationChangedMessage(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }
    }
    #endregion
}
