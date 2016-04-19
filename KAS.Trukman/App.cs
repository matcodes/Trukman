using KAS.Trukman.Messages;
using KAS.Trukman.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Trukman.Helpers;
using Trukman;

namespace KAS.Trukman
{
    #region App
    public class App : Application
	{
		static IServerManager serverManager;
		public static IServerManager ServerManager {
			get 
			{ 
				if (serverManager == null)
					serverManager = DependencyService.Get<IServerManager> ();
				return serverManager; 
			}
		}

		static ILocationService locManager;
		public static ILocationService LocManager {
			get 
			{ 
				if (locManager == null) 
					locManager = DependencyService.Get<ILocationService> ();
				return locManager; 
			}
		}


		static ILocationServicePlatformStarter locationServiceStarter;
		public static ILocationServicePlatformStarter LocationServiceStarter
		{
			get 
			{
				if (locationServiceStarter == null)
					locationServiceStarter = DependencyService.Get<ILocationServicePlatformStarter> ();
				return locationServiceStarter;
			}
		}

		public App ()
		{
			//if (!App.ServerManager.IsAuthorizedUser()) 
			this.MainPage = new ContentPage(); // NavigationPage (new SignUpTypePage ());
			/*else
				this.MainPage = new MainPage();*/

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
			base.OnStart ();

			ShowMainPageMessage.Subscribe (this, this.ShowMainMenu);
			ShowTopPageMessage.Subscribe (this, this.ShowTopPage);
			PopToRootPageMessage.Subscribe (this, this.PopToRootPage);
            PopPageMessage.Subscribe(this, this.PopPage);
            ShowOwnerSignUpWelcomePageMessage.Subscribe(this, this.ShowOwnerSignUpWelcomePage);

            ShowTopPageMessage.Send ();
		}

		private void ShowMainMenu(ShowMainPageMessage message)
		{
			this.MainPage = new MainPage ();
		}

        private async void PopPage(PopPageMessage message)
        {
            if (this.MainPage is NavigationPage)
                await (this.MainPage as NavigationPage).PopAsync();
        }

		private async void PopToRootPage(PopToRootPageMessage message)
		{
			if (this.MainPage is NavigationPage)
				await (this.MainPage as NavigationPage).PopToRootAsync();
		}

        private async void ShowOwnerSignUpWelcomePage(ShowOwnerSignUpWelcomePageMessage message)
        {
            if (this.MainPage is NavigationPage)
            {
                var page = new OwnerSignUpWelcomePage();
                page.ViewModel.Initialize(message.CompanyName);
                await (this.MainPage as NavigationPage).PushAsync(page);
            }
        }

		protected override void OnSleep ()
		{
			ShowMainPageMessage.Unsubscribe (this);
			ShowTopPageMessage.Unsubscribe (this);
            PopPageMessage.Unsubscribe(this);
			PopToRootPageMessage.Unsubscribe (this);
            ShowOwnerSignUpWelcomePageMessage.Unsubscribe(this);
        }

        protected override void OnResume ()
		{
            ShowOwnerSignUpWelcomePageMessage.Subscribe(this, this.ShowOwnerSignUpWelcomePage);
			ShowMainPageMessage.Subscribe (this, this.ShowMainMenu);
			ShowTopPageMessage.Subscribe (this, this.ShowTopPage);
			PopPageMessage.Subscribe(this, this.PopPage);
			PopToRootPageMessage.Subscribe (this, this.PopToRootPage);
		}

		private void ShowTopPage(ShowTopPageMessage message)
		{
			Device.BeginInvokeOnMainThread (async () => {
				//var _navigationPage = new NavigationPage ();
				//SettingsServiceHelper.SaveRejectedCounter(0);
				if (App.ServerManager.IsAuthorizedUser () && App.serverManager.GetCurrentUserRole () == global::Trukman.Interfaces.UserRole.UserRoleOwner) {
				} 
				else {
					string companyName = SettingsServiceHelper.GetCompany ();

					if (!App.ServerManager.IsAuthorizedUser () || string.IsNullOrEmpty (companyName)) {
						this.MainPage = new NavigationPage (new SignUpTypePage ());
					} else {
						var status = await App.serverManager.GetAuthorizationStatus (companyName);
						if (status == AuthorizationRequestStatus.Authorized)
							this.MainPage = new MainPage ();
						else if (status == AuthorizationRequestStatus.Pending)
							this.MainPage = new NavigationPage (new PendingAuthorizationPage ());
						else if (status == AuthorizationRequestStatus.Declined)
							this.MainPage = new NavigationPage (new SignUpTypePage ());
					}
				}
			});
		}
	}
    #endregion
}
