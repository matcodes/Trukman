using System;
using Xamarin.Forms;

namespace Trukman
{
	public class StartLocationServiceMessage
	{
		static string MESSAGE_KEY = "LocationStarterMessage";

		public static void Send()
		{
			StartLocationServiceMessage message = new StartLocationServiceMessage ();
			MessagingCenter.Send<StartLocationServiceMessage> (message, MESSAGE_KEY);
		}

		public static void Subscribe(object subscriber, Action<StartLocationServiceMessage> action)
		{
			MessagingCenter.Subscribe<StartLocationServiceMessage> (subscriber, MESSAGE_KEY, action);
		}

		public static void Unsubscribe(object subscriber)
		{
			MessagingCenter.Unsubscribe<StartLocationServiceMessage> (subscriber, MESSAGE_KEY);
		}

		public StartLocationServiceMessage ()
		{
		}
	}
}

