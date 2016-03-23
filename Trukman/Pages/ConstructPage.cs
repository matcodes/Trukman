using System;

using Xamarin.Forms;

namespace Trukman
{
	public class ConstructPage : BasePage
	{
		public ConstructPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);

			//Title = ""
			Content = new StackLayout { 
				Children = {
					new Label { Text = "This page is under construction" }
				}
			};
		}
	}
}


