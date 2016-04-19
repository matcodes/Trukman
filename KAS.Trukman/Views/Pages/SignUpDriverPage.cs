using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Trukman.ViewModels.Pages;
using KAS.Trukman.Views.Pages;
using KAS.Trukman.Controls;
using KAS.Trukman.Helpers;
using KAS.Trukman;
using Trukman.Helpers;
using Trukman.Interfaces;

namespace Trukman
{
	public class SignUpDriverPage : TrukmanPage
	{
		Label lblSignup;
		Label lblUserRole;
        /*
		TrukmanEditor edtFirstName;
		TrukmanEditor edtLastName;
		TrukmanEditor edtCompany;
		TrukmanEditor edtPhone;
		TrukmanButton btnSubmit;
        */
        AppEntry edtFirstName;
        AppEntry edtLastName;
        AppEntry edtCompany;
        AppEntry edtPhone;
        AppButton btnSubmit;


        Label lblHaveAccount;
		ActivityIndicator indicator;

		public SignUpDriverPage ()
		{
			this.BindingContext = new SignUpDriverViewModel ();
		}

		protected override View CreateContent ()
		{
			// TODO: для контрола edtCompany добавить фильтрацию списка компаний и выбор компании из этого списка
			// TODO: доделать проверку ввода confirmation code

			// TODO: для popuplayout: закруглить края окна и нарисовать линии как на макете
			// TODO: После проверки confirmation code, добавить popup'ы "Success" и "Invalid code"

			var btnLeft = new ToolButton ();
			btnLeft.ImageSourceName = PlatformHelper.LeftImageSource;
			btnLeft.SetBinding(ToolButton.CommandProperty, "ShowPrevPageCommand", BindingMode.OneWay);
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
			titleGrid.Children.Add (btnLeft, 0, 0);
			titleGrid.Children.Add (lblSignup, 1, 0);
			titleGrid.Children.Add (segmentLan, 2, 0);

			Image logoImage = new Image 
			{ 
				Source = ImageSource.FromFile ("logo.png"), 
				Aspect = Aspect.AspectFit,
				HorizontalOptions = LayoutOptions.Center
			};

			var logoContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 10, 20, 0),
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
				Padding = new Thickness (20, 10, 20, 10),
				Content = lblUserRole
			};

            /*
			var btnFirstName = new Button { Style = (Style)App.Current.Resources ["buttonForEntryRadiusStyle"] };
			edtFirstName = new TrukmanEditor { Style = (Style)App.Current.Resources ["entryRadiusStyle"] };
			var btnLastName = new Button { Style = (Style)App.Current.Resources ["buttonForEntryRadiusStyle"] };
			edtLastName = new TrukmanEditor { Style = (Style)App.Current.Resources ["entryRadiusStyle"] };
			var btnCompany = new Button{ Style = (Style)App.Current.Resources ["buttonForEntryRadiusStyle"] };
			edtCompany = new TrukmanEditor { Style = (Style)App.Current.Resources ["entryRadiusStyle"] };
			var btnPhone = new Button{ Style = (Style)App.Current.Resources ["buttonForEntryRadiusStyle"] };
			edtPhone = new TrukmanEditor { Style = (Style)App.Current.Resources ["entryRadiusStyle"] };

			RelativeLayout userInfoLayout = new RelativeLayout {
				HorizontalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 0, 20, 10)
			};

			userInfoLayout.Children.Add (btnFirstName, 
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToParent (parent => parent.Width)
			);
			userInfoLayout.Children.Add (edtFirstName, 
				Constraint.RelativeToView (btnFirstName, (parent, View) => View.X + Constants.ViewsPadding / 2),
				Constraint.RelativeToView (btnFirstName, (parent, View) => View.Y),
				Constraint.RelativeToView (btnFirstName, (parent, View) => View.Width - Constants.ViewsPadding),
				Constraint.RelativeToView (btnFirstName, (parent, View) => View.Height)
			);				
			userInfoLayout.Children.Add (btnLastName, 
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToView(edtFirstName, (parent, View) => View.Y + View.Height + Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => parent.Width)
			);
			userInfoLayout.Children.Add (edtLastName, 
				Constraint.RelativeToView (btnLastName, (parent, View) => View.X + Constants.ViewsPadding / 2),
				Constraint.RelativeToView (btnLastName, (parent, View) => View.Y),
				Constraint.RelativeToView (btnLastName, (parent, View) => View.Width - Constants.ViewsPadding),
				Constraint.RelativeToView (btnLastName, (parent, View) => View.Height)
			);				
			userInfoLayout.Children.Add (btnPhone, 
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToView(edtLastName, (parent, View) => View.Y + View.Height + Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => parent.Width)
			);
			userInfoLayout.Children.Add (edtPhone, 
				Constraint.RelativeToView (btnPhone, (parent, View) => View.X + Constants.ViewsPadding / 2),
				Constraint.RelativeToView (btnPhone, (parent, View) => View.Y),
				Constraint.RelativeToView (btnPhone, (parent, View) => View.Width - Constants.ViewsPadding),
				Constraint.RelativeToView (btnPhone, (parent, View) => View.Height)
			);
			userInfoLayout.Children.Add (btnCompany, 
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToView(edtPhone, (parent, View) => View.Y + View.Height + Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => parent.Width)
			);
			userInfoLayout.Children.Add (edtCompany, 
				Constraint.RelativeToView (btnCompany, (parent, View) => View.X + Constants.ViewsPadding / 2),
				Constraint.RelativeToView (btnCompany, (parent, View) => View.Y),
				Constraint.RelativeToView (btnCompany, (parent, View) => View.Width - Constants.ViewsPadding),
				Constraint.RelativeToView (btnCompany, (parent, View) => View.Height)
			);				

			var userGrid = new Grid {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				RowSpacing = 0,
				ColumnSpacing = 0,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) }
				}
			};
			userGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (20, GridUnitType.Absolute) });
			userGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) });
			userGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (20, GridUnitType.Absolute) });
			userGrid.Children.Add (userInfoLayout, 1, 0);
            */

            edtFirstName = new AppEntry {
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            edtLastName = new AppEntry {
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            edtPhone = new AppEntry {
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            edtCompany = new AppEntry {
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };

            edtFirstName.Completed += (sender, args) => {
                edtLastName.Focus();
            };
            edtLastName.Completed += (sender, args) => {
                edtPhone.Focus();
            };
            edtPhone.Completed += (sender, args) => {
                edtCompany.Focus();
            };

            var userGrid = new Grid {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 5,
                ColumnSpacing = 0,
                Padding = new Thickness(20, 0, 20, 10),
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) }
                }
            };
            userGrid.Children.Add(edtFirstName, 0, 0);
            userGrid.Children.Add(edtLastName, 0, 1);
            userGrid.Children.Add(edtPhone, 0, 2);
            userGrid.Children.Add(edtCompany, 0, 3);
           

            //btnSubmit = new TrukmanButton ();
            btnSubmit = new AppButton();
			btnSubmit.Clicked += btnSubmit_Clicked;

			var buttonContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 0, 20, 10),
				Content = btnSubmit
			};

			lblHaveAccount = new Label { HorizontalOptions = LayoutOptions.Center };
			var lblAccContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 0, 20, 0),
				Content = lblHaveAccount
			};


			indicator = new ActivityIndicator {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};

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
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Star) }
				}
			};

			content.Children.Add (titleGrid, 0, 0);
			content.Children.Add (logoContent, 0, 1);
			content.Children.Add (roleContent, 0, 2);
			content.Children.Add (userGrid, 0, 3);
			content.Children.Add (buttonContent, 0, 4);
			content.Children.Add (lblAccContent, 0, 5);

			var pageContent = new Grid {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				RowSpacing = 0,
				ColumnSpacing = 0
			};
			pageContent.Children.Add (content);
			pageContent.Children.Add (indicator);

			UpdateText ();

			return content;
		}

		void SegmentLan_ValueChanged (object sender, EventArgs e)
		{
			if (((SegmentedControl)sender).SelectedValue == ((SegmentedControl)sender).Children [0].Text)
				Localization.language = Localization.Languages.ENGLISH;
			else if (((SegmentedControl)sender).SelectedValue == ((SegmentedControl)sender).Children [1].Text)
				Localization.language = Localization.Languages.ESPANIOL;

			UpdateText ();
		}

		void UpdateText()
		{
			lblSignup.Text = Localization.getString (Localization.LocalStrings.SIGN_UP).ToUpper();
			lblUserRole.Text = Localization.getString (Localization.LocalStrings.DRIVER).ToUpper();
			edtFirstName.Placeholder = Localization.getString (Localization.LocalStrings.FIRST_NAME);
			edtLastName.Placeholder = Localization.getString (Localization.LocalStrings.LAST_NAME);
			edtPhone.Placeholder = Localization.getString (Localization.LocalStrings.PHONE);
			edtCompany.Placeholder = Localization.getString (Localization.LocalStrings.COMPANY_YOU_WORK_FOR);
			btnSubmit.Text = Localization.getString (Localization.LocalStrings.BTN_SUBMIT);
			lblHaveAccount.Text = Localization.getString (Localization.LocalStrings.HAVE_ACCOUNT_QUESTION);
		}

		View CreatePopup(PopupLayout popupLayout)
		{
			Label lblText = new Label {
				Text = Localization.getString (Localization.LocalStrings.REQUEST_CONFIRMATION_CODE),
				TextColor = Color.FromHex(Constants.GreyText)
			};

			var btnConfirmCode = new Button { Style = (Style)App.Current.Resources ["buttonForEntryRadiusStyle"] };
			var edtConfirmCode = new TrukmanEditor { 
				Placeholder = Localization.getString (Localization.LocalStrings.CONFIRMATION_CODE),
				Style = (Style)App.Current.Resources ["entryRadiusStyle"]
			};

			var btnCancel = new Button {
				Text = Localization.getString (Localization.LocalStrings.BTN_CANCEL),
				TextColor = Color.FromHex(Constants.BlueText),
				BackgroundColor = Color.Transparent
			};
			btnCancel.Clicked += delegate(object sender, EventArgs e) {
				popupLayout.DismissPopup();
			};
			var btnResend = new Button {
				Text = Localization.getString (Localization.LocalStrings.BTN_RESEND),
				TextColor = Color.FromHex(Constants.BlueText),
				BackgroundColor = Color.Transparent
			};
			btnResend.Clicked += delegate(object sender, EventArgs e) {
				popupLayout.DismissPopup();
			};
			var btnSubmitCode = new TrukmanButton {
				Text = Localization.getString (Localization.LocalStrings.BTN_SUBMIT)
			};
			btnSubmitCode.Clicked += async (object sender, EventArgs e) => 
			{
				// Подтверждаем confirmation code
				popupLayout.DismissPopup();

				await RegisterUser();
			};

			RelativeLayout relaviteLayout = new RelativeLayout {
				BackgroundColor = Color.FromHex(Constants.PopupBackgroud),
			
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
			};
			relaviteLayout.Children.Add (lblText, 
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => parent.Width - Constants.ViewsBottomPadding * 2)
			);
			relaviteLayout.Children.Add (btnConfirmCode, 
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding),
				Constraint.RelativeToView(lblText, (parent, View) => View.Y + View.Height + Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => parent.Width - Constants.ViewsBottomPadding * 2)
			);
			relaviteLayout.Children.Add (edtConfirmCode, 
				Constraint.RelativeToView(btnConfirmCode, (parent, View) => View.X + Constants.ViewsPadding / 2),
				Constraint.RelativeToView (btnConfirmCode, (parent, View) => View.Y),
				Constraint.RelativeToView (btnConfirmCode, (parent, View) => View.Width - Constants.ViewsPadding)
			);				
			relaviteLayout.Children.Add (btnSubmitCode, 
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding),
				Constraint.RelativeToView (edtConfirmCode, (parent, View) => View.Y + View.Height + Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => parent.Width - Constants.ViewsBottomPadding * 2)
			);
			relaviteLayout.Children.Add (btnCancel, 
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToView (btnSubmitCode, (parent, View) => View.Y + View.Height + Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => parent.Width / 2)
			);
			relaviteLayout.Children.Add (btnResend, 
				Constraint.RelativeToParent (parent => parent.Width / 2),
				Constraint.RelativeToView (btnSubmitCode, (parent, View) => View.Y + View.Height + Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => parent.Width / 2)
			);

			relaviteLayout.Opacity = 0.95;

			return relaviteLayout;
		}

		bool IsFrozenAuthorization()
		{
			int counterValue = SettingsServiceHelper.GetRejectCounter ();
			DateTime lastRejectedTime = SettingsServiceHelper.GetLastRejectTime ();
			int hours = (DateTime.Now - lastRejectedTime).Hours;

			// Последняя неудачная попытка была более 24-х часов назад, обнуляем счетчик
			if (hours >= 24) {
				SettingsServiceHelper.SaveRejectedCounter (0);
			}

			// Вход для пользователя заморожен
			if (counterValue >= SettingsServiceHelper.MaxRejectedRequestCount)
				return true;

			return false;
		}

		async Task RegisterUser ()
		{
			string username = string.Format ("{0} {1}", (edtFirstName.Text ?? "").Trim(), (edtLastName.Text ?? "").Trim ()).Trim();
			bool isJoinCompany;
			indicator.IsRunning = true;
			try
			{
				await App.ServerManager.Register (username, edtPhone.Text, UserRole.UserRoleDriver);
				SettingsServiceHelper.SaveCompany(edtCompany.Text);
				isJoinCompany = await App.ServerManager.RequestToJoinCompany (edtCompany.Text);
			}
			finally {
				indicator.IsRunning = false;
			}
			if (isJoinCompany) {
				ShowMainPageMessage.Send ();
				//await Navigation.PushAsync (new MainPage ());
			}
			else {
				//App.ServerManager.LogOut ();
				if (this.IsFrozenAuthorization ()) {
					await App.ServerManager.LogOut ();
					await Navigation.PushAsync (new SignupFrozenPage ());
				}
				else
					await Navigation.PushAsync (new PendingAuthorizationPage ());
			}
		}

		async void btnSubmit_Clicked (object sender, EventArgs e)
		{
			try {
				await RegisterUser();

				/*var popupLayout = this.Content as PopupLayout;

				if (popupLayout.IsPopupActive) {
					popupLayout.DismissPopup ();
				} else {
					var popup = CreatePopup (popupLayout);

					popupLayout.ShowPopup (popup, 
						Constraint.RelativeToParent(parent => Constants.ViewsPadding),
						Constraint.RelativeToParent(parent => parent.Height / 2 - popup.Height / 2),
						Constraint.RelativeToParent(parent => parent.Width - Constants.ViewsPadding * 2)
					);
				}
				*/

				// Old version
				/*bool findCompany = await App.ServerManager.FindCompany (edtCompany.Text);
				if (!findCompany)
					await AlertHandler.ShowCheckCompany (edtCompany.Text);
				else { 

				string username = string.Format ("{0} {1}", edtFirstName.Text.Trim (), edtLastName.Text.Trim ());
				await App.ServerManager.Register (username, edtPhone.Text, UserRole.UserRoleDriver);
				bool isJoinCompany = await App.ServerManager.RequestToJoinCompany (edtCompany.Text);
				if (isJoinCompany)
					await Navigation.PushAsync (new RootPage ());
				else {
					await Navigation.PushAsync (new PendingAuthorizPage ());
					//await AlertHandler.ShowAlert (string.Format ("The owner of the company {0} has not yet added you to the company", edtCompany.Text));
					//await App.ServerManager.LogOut();
				}
				}*/
			} catch (Exception exc) {
				throw exc;
				//AlertHandler.ShowAlert (exc.Message);
			}
		}
	}
}

