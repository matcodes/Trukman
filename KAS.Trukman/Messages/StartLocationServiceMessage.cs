using System;
using Xamarin.Forms;

namespace Trukman.Messages
{
	public class StartLocationServiceMessage
	{
		static string MESSAGE_KEY = "LocationStarterMessage";
		public object tag = null;

		public static void Send(object _tag)
		{
			StartLocationServiceMessage message = new StartLocationServiceMessage (_tag);
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

		public StartLocationServiceMessage (object _tag)
		{
			tag = _tag;
		}
	}
}

