using System;
using System.Collections.Generic;
using System.Text;
using Trukman.Helpers;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowSignUpOwnerCompanyPage
    public class ShowSignUpOwnerCompanyPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowSignUpOwnerCompanyPageMessage";

        public static void Send(MCInfo info)
        {
            var message = new ShowSignUpOwnerCompanyPageMessage(info);
            MessagingCenter.Send<ShowSignUpOwnerCompanyPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowSignUpOwnerCompanyPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowSignUpOwnerCompanyPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowSignUpOwnerCompanyPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowSignUpOwnerCompanyPageMessage(MCInfo info)
        {
            this.Info = info;
        }

        public MCInfo Info { get; private set; }
    }
    #endregion
}
