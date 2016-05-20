using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
 
namespace KAS.Trukman.Messages
{
    #region TakePhotoFromCameraMessage
    public class TakePhotoFromCameraMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "TakePhotoFromCameraMessage";

        public static void Send()
        {
            var message = new TakePhotoFromCameraMessage();
            MessagingCenter.Send<TakePhotoFromCameraMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<TakePhotoFromCameraMessage> action)
        {
            MessagingCenter.Subscribe<TakePhotoFromCameraMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<TakePhotoFromCameraMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public TakePhotoFromCameraMessage()
        {
        }
    }
    #endregion
}
