using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowOwnerSignUpWelcomePageMessage
    public class ShowOwnerSignUpWelcomePageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowOwnerSignUpWelcomePageMessage";

        public static void Send(string companyName)
        {
            var message = new ShowOwnerSignUpWelcomePageMessage(companyName);
            MessagingCenter.Send<ShowOwnerSignUpWelcomePageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowOwnerSignUpWelcomePageMessage> action)
        {
            MessagingCenter.Subscribe<ShowOwnerSignUpWelcomePageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowOwnerSignUpWelcomePageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowOwnerSignUpWelcomePageMessage(string companyName)
            : base()
        {
            this.CompanyName = companyName;
        }

        public string CompanyName { get; private set; }
    }
    #endregion
}
