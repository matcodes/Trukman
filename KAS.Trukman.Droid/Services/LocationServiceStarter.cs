using System;
using System.Threading.Tasks;
using Android.Content;

namespace Trukman.Droid.Services
{
	public class LocationServiceStarter
	{
		public event EventHandler<ServiceConnectedEventArgs> LocationServiceConnected = delegate {};

		protected LocationServiceConnection locationServiceConnection;

		public static LocationServiceStarter Current {
			get;
			private set;
		} 

		public LocationService LocationService
		{
			get {
				if (this.locationServiceConnection.Binder == null)
					throw new Exception ("Service not bound yet");
				return this.locationServiceConnection.Binder.Service;
			}
		}

		static LocationServiceStarter ()
		{
			Current = new LocationServiceStarter ();
		}

		protected LocationServiceStarter()
		{
			// Запускаем сервис, т.к. он блокирует процесс, то запускаем его в фоновом потоке
			new Task ( () => { 
				Android.App.Application.Context.StartService (new Intent (Android.App.Application.Context, typeof(LocationService)));

				// Создаем новое соединение к сервису
				this.locationServiceConnection = new LocationServiceConnection (null);

				// Событие вызывается, когда происходит вызов OnServiceConnected у соединения
				this.locationServiceConnection.ServiceConnected += (object sender, ServiceConnectedEventArgs e) => {
					// Извещаем MainActivity о том, что соединение установлено
					this.LocationServiceConnected ( this, e );
				};

				// Создаем объект Intent, чтобы привязаться к сервису
				Intent locationServiceIntent = new Intent (Android.App.Application.Context, typeof(LocationService));
				Android.App.Application.Context.BindService (locationServiceIntent, locationServiceConnection, Bind.AutoCreate);

			} ).Start ();
		}

		public static void StopLocationService()
		{
			if (Current.locationServiceConnection != null)
				Android.App.Application.Context.UnbindService (Current.locationServiceConnection);
			if (Current.LocationService != null)
				Current.LocationService.StopSelf ();
		}
	}
}

