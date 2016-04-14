using System;

using Xamarin.Forms;

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

			Image leftImage = new Image{ Source = ImageSource.FromFile ("left.png"), Aspect = Aspect.Fill };
			Image authorizationImage = new Image{ Source = ImageSource.FromResource ("authorize.png"), Aspect = Aspect.Fill };
			Image logoImage = new Image{ Source = ImageSource.FromResource ("logo.png"), Aspect = Aspect.AspectFit };

			lblSignup = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = Color.FromHex (Constants.TitleFontColor),
				FontSize = 33
			};

			lblUserRole = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = 18,
				TextColor = Color.FromHex (Constants.RegularFontColor)
			};

			var segmentLan = new SegmentedControl {
				Children = {
					new SegmentedControlOption{ Text = "ENG" },
					new SegmentedControlOption{ Text = "ESP"}
				}
			};
			segmentLan.ValueChanged += SegmentLan_ValueChanged;

			lblWaiting = new TrukmanLabel {
				FontSize = 18,
				HorizontalTextAlignment = TextAlignment.Center
					
			};

			//btnCancel = new TrukmanButton ();
			//btnCancel.Clicked += btnCancel_Clicked;

			RelativeLayout relativeLayout = new RelativeLayout ();

			relativeLayout.Children.Add (lblSignup, 
				Constraint.RelativeToParent (parent => parent.Width / 2 - lblSignup.Width / 2),
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding)
			);
			relativeLayout.Children.Add (leftImage, 
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding),
				Constraint.RelativeToView (lblSignup, (parent, View) => View.Height / 2),
				Constraint.RelativeToView (lblSignup, (parent, View) => View.Height / 2)
			);
			relativeLayout.Children.Add (segmentLan, 
				Constraint.RelativeToParent (parent => parent.Width - segmentLan.Width),
				Constraint.RelativeToParent (parent => 0)
			);
			relativeLayout.Children.Add (logoImage,
				Constraint.RelativeToParent (parent => parent.Width / 2 - logoImage.Width / 2),
				Constraint.RelativeToView (lblSignup, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding)
			);
			relativeLayout.Children.Add (lblUserRole,
				Constraint.RelativeToParent (parent => 0), //parent.Width / 2 - lblUserRole.Width / 2),
				Constraint.RelativeToView (logoImage, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding),
				Constraint.RelativeToParent(Parent => Parent.Width)
			);
			relativeLayout.Children.Add (authorizationImage,
				Constraint.RelativeToParent (parent => parent.Width / 2 - authorizationImage.Width / 2),
				Constraint.RelativeToView (lblUserRole, (parent, view) => view.Y + view.Height + Constants.ViewsPadding)
			);
			relativeLayout.Children.Add (lblWaiting,
				Constraint.RelativeToParent (parent => parent.Width / 2 - lblWaiting.Width / 2),
				Constraint.RelativeToView (authorizationImage, (parent, view) => view.Y + view.Height + Constants.ViewsPadding)
				//Constraint.RelativeToParent(parent => parent.Width - 
			);
			/*relativeLayout.Children.Add (btnCancel, 
				Constraint.RelativeToParent (parent => parent.Width / 2 - btnCancel.Width / 2),
				Constraint.RelativeToView (lblWaiting, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding)
			);*/
			Content = relativeLayout;

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


