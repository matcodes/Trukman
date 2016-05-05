using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowSignUpDriverDeclinedPageMessage
    public class ShowSignUpDriverDeclinedPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowSignUpDriverDeclinedPageMessage";

        public static void Send(Company company)
        {
            var message = new ShowSignUpDriverDeclinedPageMessage(company);
            MessagingCenter.Send<ShowSignUpDriverDeclinedPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowSignUpDriverDeclinedPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowSignUpDriverDeclinedPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowSignUpDriverDeclinedPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowSignUpDriverDeclinedPageMessage(Company company)
        {
            this.Company = company;
        }

        public Company Company { get; private set; }
    }
    #endregion
}
