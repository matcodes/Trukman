using System;

using Xamarin.Forms;

namespace Trukman
{
	public class App : Application
	{
		static IServerManager serverManager;
		public static IServerManager ServerManager {
			get { return serverManager; }
			set { serverManager = value; }
		}

		static IGPSManager gpsManager;
		public static IGPSManager GpsManager {
			get { return gpsManager; }
			set { gpsManager = value; }
		}

		public App ()
		{
			// Инициализация менеджера сервера (Parse.com)
			ServerManager = DependencyService.Get<IServerManager> ();
			ServerManager.Init ();

			//if (App.ServerManager.GetCurrentUserRole () == UserRole.UserRoleDriver) {
			GpsManager = DependencyService.Get<IGPSManager> ();
			GpsManager.InitializeLocationManager ();
			//}

			// The root page of your application
			MainPage = new NavigationPage ();

			MainPage.Navigation.PushAsync (new SignUpTypePage ());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

