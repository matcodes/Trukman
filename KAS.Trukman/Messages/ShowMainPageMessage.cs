using System;
using Xamarin.Forms;

namespace KAS.Trukman
{
	public class ShowMainPageMessage
	{
		#region Static members
		private static readonly string MESSAGE_KEY = "ShowMainPageMessage";

		public static void Send()
		{
			var message = new ShowMainPageMessage();
			MessagingCenter.Send<ShowMainPageMessage>(message, MESSAGE_KEY);
		}

		public static void Subscribe(object subscriber, Action<ShowMainPageMessage> action)
		{
			MessagingCenter.Subscribe<ShowMainPageMessage>(subscriber, MESSAGE_KEY, action);
		}

		public static void Unsubscribe(object subscriber)
		{
			MessagingCenter.Unsubscribe<ShowMainPageMessage>(subscriber, MESSAGE_KEY);
		}
		#endregion

		public ShowMainPageMessage()
		{
		}
	}
}

