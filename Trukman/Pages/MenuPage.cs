using System;
using Xamarin.Forms;

namespace Trukman
{
	public class MenuPage : MasterDetailPage
	{
		public MenuPage ()
		{
			this.Master = new ContentPage {
				Title = "",
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							Text = "Here will be the menu"
						}
					}
				}
			};

			this.Detail = new ContentPage {
				Title = "",
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							Text = "Detail Page"
						}
					}
				}
			};

		}
	}
}

