using System;
using Android.Content;

namespace Trukman.Droid
{
	public class LocationServiceConnection:Java.Lang.Object, IServiceConnection
	{
		public event EventHandler<ServiceConnectedEventArgs> ServiceConnected = delegate {};

		public LocationServiceBinder Binder
		{
			get { return this.binder; }
			set { this.binder = value; }
		}
		protected LocationServiceBinder binder;

		public LocationServiceConnection (LocationServiceBinder _binder)
		{
			if (_binder != null)
				this.binder = _binder;
		}

		// Этот метод вызывается, когда клиент пытается привязаться к сервису с помощью Intent и IServiceConnection
		public void OnServiceConnected (ComponentName name, Android.OS.IBinder service)
		{
			LocationServiceBinder serviceBinder = service as LocationServiceBinder;
			if (serviceBinder != null) {
				this.binder = serviceBinder;
				this.binder.IsBound = true;
				// raise the service connected event
				this.ServiceConnected(this, new ServiceConnectedEventArgs () { Binder = service } );

				// now that the Service is bound, we can start gathering some location data
				serviceBinder.Service.StartLocationUpdates();
			}		
		}

		public void OnServiceDisconnected (ComponentName name)
		{
			this.binder.IsBound = false;
		}
	}
}

