using System;
using Android.OS;

namespace Trukman.Droid
{
	public class ServiceConnectedEventArgs : EventArgs
	{
		public IBinder Binder { get; set; }
	}
}

