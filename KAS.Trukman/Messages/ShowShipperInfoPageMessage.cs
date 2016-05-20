using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowShipperInfoPageMessage
    public class ShowShipperInfoPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowShipperInfoPageMessage";

        public static void Send(Contractor shipper)
        {
            var message = new ShowShipperInfoPageMessage(shipper);
            MessagingCenter.Send<ShowShipperInfoPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowShipperInfoPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowShipperInfoPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowShipperInfoPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowShipperInfoPageMessage(Contractor shipper)
            : base()
        {
            this.Shipper = shipper;
        }

        public Contractor Shipper { get; private set; }
    }
    #endregion
}
