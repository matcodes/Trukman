using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace KAS.Trukman
{
	public class ChangeLocationMessage
	{
		#region Static members
		private static readonly string MESSAGE_KEY = "ShowCameraMessage";

		public static void Send(Position position)
		{
			var message = new ChangeLocationMessage(position);
			MessagingCenter.Send<ChangeLocationMessage>(message, MESSAGE_KEY);
		}

		public static void Subscribe(object subscriber, Action<ChangeLocationMessage> action)
		{
			MessagingCenter.Subscribe<ChangeLocationMessage>(subscriber, MESSAGE_KEY, action);
		}

		public static void Unsubscribe(object subscriber)
		{
			MessagingCenter.Unsubscribe<ChangeLocationMessage>(subscriber, MESSAGE_KEY);
		}
		#endregion

		public ChangeLocationMessage (Position position)
		{
			this.Position = position;
		}

		public Position Position { get; private set; }
	}
}

