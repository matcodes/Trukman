using System;

using Xamarin.Forms;

namespace Trukman
{
	public class WaitingLayout : ContentPage
	{
		// Макет для ожидания отклика страницы...

		public WaitingLayout ()
		{
			var stackLayout = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
			stackLayout.Opacity = 0.9;

			Content = stackLayout;
		}
	}
}


