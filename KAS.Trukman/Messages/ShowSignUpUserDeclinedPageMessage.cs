using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowSignUpUserDeclinedPageMessage
    public class ShowSignUpUserDeclinedPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowSignUpUserDeclinedPageMessage";

        public static void Send(Company company)
        {
            var message = new ShowSignUpUserDeclinedPageMessage(company);
            MessagingCenter.Send<ShowSignUpUserDeclinedPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowSignUpUserDeclinedPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowSignUpUserDeclinedPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowSignUpUserDeclinedPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowSignUpUserDeclinedPageMessage(Company company)
        {
            this.Company = company;
        }

        public Company Company { get; private set; }
    }
    #endregion
}
