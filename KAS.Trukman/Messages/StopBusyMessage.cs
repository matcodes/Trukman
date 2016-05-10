using System;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
	#region StopBusyMessage
	public class StopBusyMessage
	{
		#region Static members
		static string MESSAGE_KEY = "StopBusyMessage";

		public static void Send()
		{
			StopBusyMessage message = new StopBusyMessage ();
			MessagingCenter.Send<StopBusyMessage> (message, MESSAGE_KEY);
		}

		public static void Subscribe(object subscriber, Action<StopBusyMessage> action)
		{
			MessagingCenter.Subscribe<StopBusyMessage> (subscriber, MESSAGE_KEY, action);
		}

		public static void Unsubscribe(object subscriber)
		{
			MessagingCenter.Unsubscribe<StopBusyMessage> (subscriber, MESSAGE_KEY);
		}
		#endregion

		public StopBusyMessage ()
		{
		}
	}
	#endregion
}

