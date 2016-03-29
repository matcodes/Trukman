using System;

using Xamarin.Forms;

namespace Trukman
{
	public class SignupFrozenPage : BasePage
	{
		public SignupFrozenPage ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}


