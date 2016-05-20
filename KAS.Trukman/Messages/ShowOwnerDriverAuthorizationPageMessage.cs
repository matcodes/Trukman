﻿using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowOwnerDriverAuthorizationPageMessage
    public class ShowOwnerDriverAuthorizationPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowOwnerDriverAuthorizationPageMessage";

        public static void Send(string companyName, User driver)
        {
            var message = new ShowOwnerDriverAuthorizationPageMessage(companyName, driver);
            MessagingCenter.Send<ShowOwnerDriverAuthorizationPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowOwnerDriverAuthorizationPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowOwnerDriverAuthorizationPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowOwnerDriverAuthorizationPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowOwnerDriverAuthorizationPageMessage(string companyName, User driver)
        {
            this.CompanyName = companyName;
            this.Driver = driver;
        }

        public string CompanyName { get; private set; }

        public User Driver { get; private set; }
    }
    #endregion
}
