using System;
using Xamarin.Forms;

namespace Trukman.Messages
{
	public class StopLocationServiceMessage
	{
		static string MESSAGE_KEY = "LocationStopMessage";

		public static void Send()
		{
			StopLocationServiceMessage message = new StopLocationServiceMessage ();
			MessagingCenter.Send<StopLocationServiceMessage> (message, MESSAGE_KEY);
		}

		public static void Subscribe(object subscriber, Action<StopLocationServiceMessage> action)
		{
			MessagingCenter.Subscribe<StopLocationServiceMessage> (subscriber, MESSAGE_KEY, action);
		}

		public static void Unsubscribe(object subscriber)
		{
			MessagingCenter.Unsubscribe<StopLocationServiceMessage> (subscriber, MESSAGE_KEY);
		}

		public StopLocationServiceMessage ()
		{
		}
	}
}

