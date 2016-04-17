using System;
using Xamarin.Forms;

namespace KAS.Trukman
{
	public class ShowCameraMessage
	{
		#region Static members
		private static readonly string MESSAGE_KEY = "ShowCameraMessage";

		public static void Send()
		{
			var message = new ShowCameraMessage();
			MessagingCenter.Send<ShowCameraMessage>(message, MESSAGE_KEY);
		}

		public static void Subscribe(object subscriber, Action<ShowCameraMessage> action)
		{
			MessagingCenter.Subscribe<ShowCameraMessage>(subscriber, MESSAGE_KEY, action);
		}

		public static void Unsubscribe(object subscriber)
		{
			MessagingCenter.Unsubscribe<ShowCameraMessage>(subscriber, MESSAGE_KEY);
		}
		#endregion

		public ShowCameraMessage()
		{
		}
	}
}

