using System;
using Xamarin.Forms;

namespace Trukman
{
	public class ShowDriverListMessage
	{
		#region Static members
		private static readonly string MESSAGE_KEY = "ShowDriverListMessage";

		public static void Send()
		{
			ShowDriverListMessage message = new ShowDriverListMessage();
			MessagingCenter.Send<ShowDriverListMessage>(message, MESSAGE_KEY);
		}

		public static void Subscribe(object subscriber, Action<ShowDriverListMessage> action)
		{
			MessagingCenter.Subscribe (subscriber, MESSAGE_KEY, action);
		}

		public static void Unsubscribe(object subscriber)
		{
			MessagingCenter.Unsubscribe<ShowDriverListMessage> (subscriber, MESSAGE_KEY);
		}

		#endregion
	}
}

