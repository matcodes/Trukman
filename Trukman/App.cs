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

		static ILocationService locManager;
		public static ILocationService LocManager {
			get 
			{ 
				if (locManager == null) 
					locManager = DependencyService.Get<ILocationService> ();
				return locManager; 
			}
			//set { locManager = value; }
		}

		public App ()
		{
			// Инициализация менеджера сервера (Parse.com)
			ServerManager = DependencyService.Get<IServerManager> ();
			ServerManager.Init ();

			// The root page of your application
			MainPage = new NavigationPage ();

			if (!ServerManager.IsAuthorized ())
				MainPage.Navigation.PushAsync (new SignUpTypePage ());
			else
				MainPage.Navigation.PushAsync (new RootPage ());

			//if (App.ServerManager.GetCurrentUserRole () == UserRole.UserRoleDriver) {
				//LocManager = DependencyService.Get<ILocationService> ();
				//LocManager.StartLocationUpdates ();
			//}


			CreateStyles ();
		}

		void CreateStyles()
		{
			var entryRadiusStyle = new Style (typeof(TrukmanEditor)) {
				Setters = {
					new Setter{ Property = TrukmanEditor.TextColorProperty, Value = Color.FromHex ("8D8D8D") },
					new Setter{ Property = TrukmanEditor.PlaceholderColorProperty, Value = Color.FromHex ("8D8D8D") },
					new Setter{ Property = TrukmanEditor.BackgroundColorProperty, Value = Color.Transparent },
				}
			};
			var buttonForEntryRadiusStyle = new Style (typeof(TrukmanButton)) {
				Setters = {
					new Setter{ Property = TrukmanButton.TextColorProperty, Value = Color.FromHex ("000000") },
					new Setter{ Property = TrukmanButton.BackgroundColorProperty, Value = Color.FromHex ("FFFFFF") },
					new Setter{ Property = TrukmanButton.BorderRadiusProperty, Value = 22 },
				}
			};

			Resources = new ResourceDictionary ();
			Resources.Add ("entryRadiusStyle", entryRadiusStyle);
			Resources.Add ("buttonForEntryRadiusStyle", buttonForEntryRadiusStyle);
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

