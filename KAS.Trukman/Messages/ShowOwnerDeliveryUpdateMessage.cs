using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
	#region ShowOwnerDeliveryUpdateMessage

	public class ShowOwnerDeliveryUpdateMessage
	{
		#region Static members
		private static readonly string MESSAGE_KEY = "ShowOwnerDeliveryUpdateMessage";

		public static void Send()
		{
			var message = new ShowOwnerDeliveryUpdateMessage();
			MessagingCenter.Send<ShowOwnerDeliveryUpdateMessage>(message, MESSAGE_KEY);
		}

		public static void Subscribe(object subscriber, Action<ShowOwnerDeliveryUpdateMessage> action)
		{
			MessagingCenter.Subscribe<ShowOwnerDeliveryUpdateMessage>(subscriber, MESSAGE_KEY, action);
		}

		public static void Unsubscribe(object subscriber)
		{
			MessagingCenter.Unsubscribe<ShowOwnerDeliveryUpdateMessage>(subscriber, MESSAGE_KEY);
		}
		#endregion

		public ShowOwnerDeliveryUpdateMessage()
		{
		}
	}
	#endregion
}

