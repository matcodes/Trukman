using System;

using Xamarin.Forms;

namespace Trukman
{
	public class BasePage : ContentPage
	{
		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			NavigationPage.SetHasNavigationBar (this, false);
		}

		public BasePage ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello CodfasntentPage" }
				}
			};
		}
	}
}


