using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Trukman
{
	public class DispatchListPage : BasePage
	{
		ListView listView;
		IList<Trukman.User> dispatchList;

		public DispatchListPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);

			Title = "Dispatchers";

			var template = new DataTemplate (typeof(MenuItemCell));
			template.SetBinding (MenuItemCell.UserNameProperty, "UserName");
			template.SetBinding (MenuItemCell.EmailProperty, "Email");

			listView = new ListView ();
			listView.Header = new StackLayout {
				Children = {
					new Label {
						Text = "Dispatcher list",
						FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label))
					}
				}
			};

			listView.BackgroundColor = Color.Transparent;
			listView.SeparatorColor = Color.Blue;
			//listView.

			listView.SeparatorVisibility = SeparatorVisibility.Default;
			listView.IsPullToRefreshEnabled = true;
			listView.ItemTemplate = template;

			//listView.Style.Behaviors[0]. = new Style (){ };

			//listView.
			//listView.ItemSelected += async (sender, e) => { };
			//listView.ItemTapped += async (sender, e) => { };

			Content = new StackLayout { 
				//VerticalOptions = LayoutOptions.CenterAndExpand,
				Spacing = Constants.StackLayoutDefaultSpacing,
				//Padding = new Thickness (Constants.ViewsPadding),
				Children = {
					listView
				}
			};


			LoadDispatcherList ();
		}

		async void LoadDispatcherList ()
		{
			string companyName = SettingsServiceHelper.GetCompany ();
			dispatchList = await App.ServerManager.GetDispatchList (companyName);

			Device.BeginInvokeOnMainThread (() =>
				{
					listView.ItemsSource = dispatchList;
				
					// TODO: убрать потом, когда переделаю на Command'ы
					MenuItemCell.SourceList = dispatchList;
				});
		}
	}
}


