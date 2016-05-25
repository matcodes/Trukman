using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowOwnerDeliveryUpdatePageMessage
    public class ShowOwnerDeliveryUpdatePageMessage
	{
		#region Static members
		private static readonly string MESSAGE_KEY = "ShowOwnerDeliveryUpdatePageMessage";

		public static void Send()
		{
			var message = new ShowOwnerDeliveryUpdatePageMessage();
			MessagingCenter.Send<ShowOwnerDeliveryUpdatePageMessage>(message, MESSAGE_KEY);
		}

		public static void Subscribe(object subscriber, Action<ShowOwnerDeliveryUpdatePageMessage> action)
		{
			MessagingCenter.Subscribe<ShowOwnerDeliveryUpdatePageMessage>(subscriber, MESSAGE_KEY, action);
		}

		public static void Unsubscribe(object subscriber)
		{
			MessagingCenter.Unsubscribe<ShowOwnerDeliveryUpdatePageMessage>(subscriber, MESSAGE_KEY);
		}
		#endregion

		public ShowOwnerDeliveryUpdatePageMessage()
		{
		}
	}
	#endregion
}

