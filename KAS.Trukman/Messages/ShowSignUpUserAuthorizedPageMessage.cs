using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowSignUpUserAuthorizedPageMessage
    public class ShowSignUpUserAuthorizedPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowSignUpUserAuthorizedPageMessage";

        public static void Send(Company company)
        {
            var message = new ShowSignUpUserAuthorizedPageMessage(company);
            MessagingCenter.Send<ShowSignUpUserAuthorizedPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowSignUpUserAuthorizedPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowSignUpUserAuthorizedPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowSignUpUserAuthorizedPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowSignUpUserAuthorizedPageMessage(Company company)
        {
            this.Company = company;
        }

        public Company Company { get; private set; }
    }
    #endregion
}
