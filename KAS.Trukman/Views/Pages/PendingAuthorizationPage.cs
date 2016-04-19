using System;

using Xamarin.Forms;
using Trukman.Helpers;
using KAS.Trukman;
using KAS.Trukman.Helpers;

namespace Trukman
{
	public class PendingAuthorizationPage : BasePage
	{
		Label lblSignup;
		Label lblUserRole;
		//TrukmanButton btnCancel;
		Label lblWaiting;

		Timer timerForWaitingAuthorization;
		int refreshTime = 10 * 1000; // Обновление запроса каждые 10 сек.

		public PendingAuthorizationPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);

			Image leftImage = new Image{ Source = "left.png", Aspect = Aspect.Fill };

			lblSignup = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = Color.FromHex (Constants.TitleFontColor),
				FontSize = 33
			};

			var segmentLan = new SegmentedControl {
				Children = {
					new SegmentedControlOption{ Text = "ENG" },
					new SegmentedControlOption{ Text = "ESP"}
				}
			};
			segmentLan.ValueChanged += SegmentLan_ValueChanged;

			var titleGrid = new Grid {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				RowSpacing = 0,
				ColumnSpacing = 0,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) }
				}
			};
			titleGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (PlatformHelper.ActionBarHeight, GridUnitType.Absolute) });
			titleGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) });
			titleGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (PlatformHelper.ActionBarHeight * 2, GridUnitType.Absolute) });
			titleGrid.Children.Add (leftImage, 0, 0);
			titleGrid.Children.Add (lblSignup, 1, 0);
			titleGrid.Children.Add (segmentLan, 2, 0);

			Image logoImage = new Image 
			{ 
				Source = ImageSource.FromFile ("logo.png"), 
				Aspect = Aspect.AspectFit,
				HorizontalOptions = LayoutOptions.Center,
				WidthRequest = 100,
				HeightRequest = 100
			};

			var logoContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 20, 20, 20),
				Content = logoImage
			};

			lblUserRole = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = 18,
				TextColor = Color.FromHex (Constants.RegularFontColor)
			};

			var roleContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 20, 20, 20),
				Content = lblUserRole
			};

			Image authorizationImage = new Image{ Source = "authorize.png"/*, Aspect = Aspect.Fill*/ };
			var authContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 20, 20, 20),
				Content = authorizationImage
			};

			lblWaiting = new TrukmanLabel {
				HorizontalOptions = LayoutOptions.Center,
				FontSize = 18
			};

			var waitContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 20, 20, 20),
				Content = lblWaiting
			};

			//btnCancel = new TrukmanButton ();
			//btnCancel.Clicked += btnCancel_Clicked;

			var content = new Grid {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				RowSpacing = 0,
				ColumnSpacing = 0,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Star) }
				}
			};

			content.Children.Add (titleGrid, 0, 0);
			content.Children.Add (logoContent, 0, 1);
			content.Children.Add (roleContent, 0, 2);
			content.Children.Add (authContent, 0, 3);
			content.Children.Add (waitContent, 0, 4);

			Content = content;

			UpdateText ();

			TimerCallback callback = new TimerCallback (IsAuthorized);
			timerForWaitingAuthorization = new Timer (callback, null, 0, refreshTime);
		}

		async void IsAuthorized(object state)
		{
			string company = SettingsServiceHelper.GetCompany ();

			// Проверяем был ли пользователь (водитель/диспетчер) авторизован владельцем компании
			var status = await App.ServerManager.GetAuthorizationStatus (company);

			//status = AuthorizationRequestStatus.Frozen;

			if (status== AuthorizationRequestStatus.Authorized) {
				SettingsServiceHelper.SaveRejectedCounter (0);

				Device.BeginInvokeOnMainThread(async () => 
					{
						timerForWaitingAuthorization.Cancel();
						await Navigation.PushAsync (new AuthorizedPage ());
					});
			}
			else if (status == AuthorizationRequestStatus.Declined) {
				int counterValue = SettingsServiceHelper.GetRejectCounter ();
				counterValue++;

				SettingsServiceHelper.SaveRejectedCounter (counterValue);
				SettingsServiceHelper.SaveLastRejectTime (DateTime.Now);

				if (counterValue >= SettingsServiceHelper.MaxRejectedRequestCount) 
				{
					Device.BeginInvokeOnMainThread(async () => 
						{
							timerForWaitingAuthorization.Cancel();
							await Navigation.PushAsync(new SignupFrozenPage());
						});
				}
				else {
					Device.BeginInvokeOnMainThread(async () => 
						{
							timerForWaitingAuthorization.Cancel();
							await Navigation.PushAsync (new AuthorizationDeclinedPage ());
						});
				}

				await App.ServerManager.LogOut ();
			}
		}

		/*void btnCancel_Clicked (object sender, EventArgs e)
		{
			Navigation.PopAsync ();			
		}*/

		void UpdateText()
		{
			lblSignup.Text = Localization.getString (Localization.LocalStrings.SIGN_UP).ToUpper();
			lblUserRole.Text = Localization.getString (Localization.LocalStrings.DRIVER).ToUpper();
			lblWaiting.Text = string.Format (Localization.getString (Localization.LocalStrings.WAITING_FOR_COMPANY_TO_AUTHORIZE), SettingsServiceHelper.GetCompany ());
			//btnCancel.Text = Localization.getString (Localization.LocalStrings.CANCEL_AUTHORIZATION_REQUEST);
		}

		void SegmentLan_ValueChanged (object sender, EventArgs e)
		{
			if (((SegmentedControl)sender).SelectedValue == ((SegmentedControl)sender).Children [0].Text)
				Localization.language = Localization.Languages.ENGLISH;
			else if (((SegmentedControl)sender).SelectedValue == ((SegmentedControl)sender).Children [1].Text)
				Localization.language = Localization.Languages.ESPANIOL;

			UpdateText ();
		}
	}
}


