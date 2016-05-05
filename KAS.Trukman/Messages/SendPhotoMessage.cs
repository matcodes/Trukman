using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region SendPhotoMessage
    public class SendPhotoMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "SendPhotoMessage";

        public static void Send(byte[] data)
        {
            var message = new SendPhotoMessage(data);
            MessagingCenter.Send<SendPhotoMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<SendPhotoMessage> action)
        {
            MessagingCenter.Subscribe<SendPhotoMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<SendPhotoMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public SendPhotoMessage(byte[] data)
        {
            this.Data = data;
        }

        public byte[] Data { get; private set; }
    }
    #endregion
}
