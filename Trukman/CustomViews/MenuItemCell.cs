using System;
using Xamarin.Forms;
using System.Collections;
using System.Collections.Generic;

namespace Trukman
{
	public class MenuItemCell: ViewCell
	{
		Label userNameLabel;
		Label emailLabel;

		//TODO: заменить на Command
		public static IList<Trukman.User> SourceList;

		public static readonly BindableProperty UserNameProperty = BindableProperty.Create("UserName", typeof(string), typeof(MenuItemCell), "UserName");
		public static readonly BindableProperty EmailProperty = BindableProperty.Create("Email", typeof(string), typeof(MenuItemCell), "Email");

		public string UserName { get { return (string)GetValue(UserNameProperty); } set { SetValue(UserNameProperty, value); } }
		public string Email { get { return (string)GetValue(EmailProperty); } set { SetValue(EmailProperty, value); } }

		public MenuItemCell ()
		{
			StackLayout layout = new StackLayout ();
			layout.Padding = new Thickness (15, 0);

			userNameLabel = new Label (){ FontAttributes = FontAttributes.Bold };
			emailLabel = new Label ();

			layout.Children.Add (userNameLabel);
			layout.Children.Add (emailLabel);

			var moreItem = new MenuItem { Text = "More" };
			moreItem.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			moreItem.Clicked += async (sender, e) => {
				await AlertHandler.ShowAlert("Don't implement more operation");
			};

			//moreItem.lon

			var removeItem = new MenuItem{ Text = "Remove", IsDestructive = true };
			//removeItem.
			removeItem.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			removeItem.Clicked += async (sender, e) => {
				//(sender as MenuItem).

				var user = ((sender as MenuItem).CommandParameter as Trukman.User);
				if (user != null)
				{
					if (await AlertHandler.ShowCheckRemoveUser(user.UserName))
					{
						SourceList.Remove(user);

						string companyName = SettingsServiceHelper.GetCompany();
						await App.ServerManager.RemoveCompanyUser(companyName, user);
					}
				}
			};

			ContextActions.Add (moreItem);
			ContextActions.Add (removeItem);

			View = layout;
		}

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();

			if (BindingContext != null) {
				userNameLabel.Text = UserName;
				emailLabel.Text = Email;
			}
		}
	}
}

