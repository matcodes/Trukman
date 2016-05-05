using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowSignUpOwnerWelcomePageMessage
    public class ShowSignUpOwnerWelcomePageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowSignUpOwnerWelcomePageMessage";

        public static void Send(Company company)
        {
            var message = new ShowSignUpOwnerWelcomePageMessage(company);
            MessagingCenter.Send<ShowSignUpOwnerWelcomePageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowSignUpOwnerWelcomePageMessage> action)
        {
            MessagingCenter.Subscribe<ShowSignUpOwnerWelcomePageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowSignUpOwnerWelcomePageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowSignUpOwnerWelcomePageMessage(Company company)
            : base()
        {
            this.Company = company;
        }

        public Company Company { get; private set; }
    }
    #endregion
}
