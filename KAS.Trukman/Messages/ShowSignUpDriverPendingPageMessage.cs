using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowSignUpDriverPendingPageMessage
    public class ShowSignUpDriverPendingPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowSignUpDriverPendingPageMessage";

        public static void Send(Company company)
        {
            var message = new ShowSignUpDriverPendingPageMessage(company);
            MessagingCenter.Send<ShowSignUpDriverPendingPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowSignUpDriverPendingPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowSignUpDriverPendingPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowSignUpDriverPendingPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowSignUpDriverPendingPageMessage(Company company)
        {
            this.Company = company;
        }

        public Company Company { get; private set; }
    }
    #endregion
}
