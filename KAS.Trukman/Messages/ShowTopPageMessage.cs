using System;
using Xamarin.Forms;

namespace KAS.Trukman
{
	public class ShowTopPageMessage
	{
		#region Static members
		private static readonly string MESSAGE_KEY = "ShowTopPageMessage";

		public static void Send()
		{
			var message = new ShowTopPageMessage();
			MessagingCenter.Send<ShowTopPageMessage>(message, MESSAGE_KEY);
		}

		public static void Subscribe(object subscriber, Action<ShowTopPageMessage> action)
		{
			MessagingCenter.Subscribe<ShowTopPageMessage>(subscriber, MESSAGE_KEY, action);
		}

		public static void Unsubscribe(object subscriber)
		{
			MessagingCenter.Unsubscribe<ShowTopPageMessage>(subscriber, MESSAGE_KEY);
		}
		#endregion

		public ShowTopPageMessage ()
		{
		}
	}
}

