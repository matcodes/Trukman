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
					new Setter{ Property = TrukmanButton.TextColorProperty, Value = Color.Black },
					new Setter{ Property = TrukmanButton.BackgroundColorProperty, Value = Color.White },
					new Setter{ Property = TrukmanButton.BorderRadiusProperty, Value = 22 },
				}
			};

            var disabledEntryStyle = new Style (typeof(TrukmanEditor)) {
                Setters = {
                    new Setter{ Property = TrukmanEditor.TextColorProperty, Value = Color.Black },
                    new Setter{ Property = TrukmanEditor.BackgroundColorProperty, Value = Color.Transparent },
                }
            };

            var disabledButtonForEntryRadiusStyle = new Style (typeof(TrukmanButton)) {
                Setters = {
                    new Setter{ Property = TrukmanButton.TextColorProperty, Value = Color.FromHex ("7A7474") },
                    new Setter{ Property = TrukmanButton.BackgroundColorProperty, Value = Color.FromHex ("EAD2D2") },
                    new Setter{ Property = TrukmanButton.BorderRadiusProperty, Value = 22 }
                }
            };

            var buttonTransparentEntry = new Style(typeof(TrukmanButton)) {
                Setters = {
                    new Setter{ Property = TrukmanButton.BackgroundColorProperty, Value = Color.Transparent },
                    new Setter{ Property = TrukmanButton.BorderRadiusProperty, Value = 22 },
                    new Setter{ Property = TrukmanButton.BorderColorProperty, Value = Color.White },
                    new Setter{ Property = TrukmanButton.BorderWidthProperty, Value = 1.5 }
                }
            };

			Resources = new ResourceDictionary ();
			Resources.Add ("entryRadiusStyle", entryRadiusStyle);
			Resources.Add ("buttonForEntryRadiusStyle", buttonForEntryRadiusStyle);
            Resources.Add ("disabledEntryStyle", disabledEntryStyle);
            Resources.Add ("disabledButtonForEntryRadiusStyle", disabledButtonForEntryRadiusStyle);
            Resources.Add ("buttonTransparentEntry", buttonTransparentEntry);
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

