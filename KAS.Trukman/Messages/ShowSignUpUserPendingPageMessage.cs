using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowSignUpUserPendingPageMessage
    public class ShowSignUpUserPendingPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowSignUpUserPendingPageMessage";

        public static void Send(Company company)
        {
            var message = new ShowSignUpUserPendingPageMessage(company);
            MessagingCenter.Send<ShowSignUpUserPendingPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowSignUpUserPendingPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowSignUpUserPendingPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowSignUpUserPendingPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowSignUpUserPendingPageMessage(Company company)
        {
            this.Company = company;
        }

        public Company Company { get; private set; }
    }
    #endregion
}
