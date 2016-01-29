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

		public App ()
		{
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

