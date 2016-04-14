using System;

using Xamarin.Forms;
using Trukman.Messages;

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

		private NavigationPage _navigationPage = null;

		public App ()
		{
			// Инициализация менеджера сервера (Parse.com)
			ServerManager = DependencyService.Get<IServerManager> ();
			ServerManager.Init ();

			// The root page of your application
			MainPage = new NavigationPage ();

			if (!App.ServerManager.IsAuthorizedUser())
				_navigationPage = new NavigationPage(new SignUpTypePage ());
			else
				_navigationPage = new NavigationPage(new HomePage ());

			this.MainPage = _navigationPage;
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

		private void PushPage(Page page)
		{
			Device.BeginInvokeOnMainThread(async () => {
				await _navigationPage.PushAsync(page);
			});
		}

		private void PopPage()
		{
			Device.BeginInvokeOnMainThread(async () =>
				{
					await _navigationPage.PopAsync(true);
				});
		}

		private void PopToRootPage()
		{
			Device.BeginInvokeOnMainThread(async () => 
				{
					await _navigationPage.PopToRootAsync(true);
				});
		}

		private void SubscribeMessages()
		{
			PopPageMessage.Subscribe(this, this.PopPage);
			PopToRootPageMessage.Subscribe(this, this.PopToRootPage);
			ShowShipperInfoPageMessage.Subscribe(this, this.ShowShipperInfoPage);
			ShowReceiverInfoPageMessage.Subscribe(this, this.ShowReceiverInfoPage);
			ShowFuelAdvancePageMessage.Subscribe(this, this.ShowFuelAdvancePage);
			StartLocationServiceMessage.Subscribe (this, this.StartLocationService);
			ShowDriverListMessage.Subscribe (this, this.ShowDriverListPage);
		}

		private void UnsubscribeMessages()
		{
			PopPageMessage.Unsubscribe(this);
			PopToRootPageMessage.Unsubscribe(this);
			ShowShipperInfoPageMessage.Unsubscribe(this);
			ShowReceiverInfoPageMessage.Unsubscribe(this);
			ShowFuelAdvancePageMessage.Unsubscribe(this);
			StartLocationServiceMessage.Unsubscribe (this);
			ShowDriverListMessage.Unsubscribe (this);
		}

		protected override void OnStart ()
		{
			this.SubscribeMessages();
		}

		protected override void OnSleep ()
		{
			this.UnsubscribeMessages();
		}

		protected override void OnResume ()
		{
			this.SubscribeMessages();
		}

		private void PopPage(PopPageMessage message)
		{
			this.PopPage();
		}

		private void PopToRootPage(PopToRootPageMessage message)
		{
			this.PopToRootPage();
		}

		private void ShowShipperInfoPage(ShowShipperInfoPageMessage message)
		{
			var shipperInfoPage = new ShipperInfoPage();
			shipperInfoPage.ViewModel.Initialize(message.Shipper);
			this.PushPage(shipperInfoPage);
		}

		private void ShowReceiverInfoPage(ShowReceiverInfoPageMessage message)
		{
			var receiverInfoPage = new ReceiverInfoPage();
			receiverInfoPage.ViewModel.Initialize(message.Receiver);
			this.PushPage(receiverInfoPage);
		}

		private void ShowFuelAdvancePage(ShowFuelAdvancePageMessage message)
		{
			var fuelAdvancePage = new FuelAdvancePage();
			fuelAdvancePage.ViewModel.Initialize();
			this.PushPage(fuelAdvancePage);
		}

		private void StartLocationService(StartLocationServiceMessage message)
		{
			LocationServiceStarter.StartService ();
		}

		private void ShowDriverListPage(ShowDriverListMessage message)
		{
			var driverListPage = new DriverListPage ();
			driverListPage.ViewModel.Initialize ();
			this.PushPage (driverListPage);
		}
	}
}

