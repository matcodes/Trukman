using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowSignUpDriverAuthorizedPageMessage
    public class ShowSignUpDriverAuthorizedPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowSignUpDriverAuthorizedPageMessage";

        public static void Send(Company company)
        {
            var message = new ShowSignUpDriverAuthorizedPageMessage(company);
            MessagingCenter.Send<ShowSignUpDriverAuthorizedPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowSignUpDriverAuthorizedPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowSignUpDriverAuthorizedPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowSignUpDriverAuthorizedPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowSignUpDriverAuthorizedPageMessage(Company company)
        {
            this.Company = company;
        }

        public Company Company { get; private set; }
    }
    #endregion
}
