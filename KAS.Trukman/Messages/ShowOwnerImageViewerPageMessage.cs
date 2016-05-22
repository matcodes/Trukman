using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowOwnerImageViewerPageMessage
    public class ShowOwnerImageViewerPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowOwnerImageViewerPageMessage";

        public static void Send(Photo photo)
        {
            var message = new ShowOwnerImageViewerPageMessage(photo);
            MessagingCenter.Send<ShowOwnerImageViewerPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowOwnerImageViewerPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowOwnerImageViewerPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowOwnerImageViewerPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowOwnerImageViewerPageMessage(Photo photo)
        {
            this.Photo = photo;
        }

        public Photo Photo { get; private set; }
    }
    #endregion
}
