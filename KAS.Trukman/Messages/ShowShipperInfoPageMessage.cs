using KAS.Trukman.Data.Interfaces;
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

        public static void Send(IContractor shipper)
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

        public ShowShipperInfoPageMessage(IContractor shipper)
            : base()
        {
            this.Shipper = shipper;
        }

        public IContractor Shipper { get; private set; }
    }
    #endregion
}
