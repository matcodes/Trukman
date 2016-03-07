using System;

using Xamarin.Forms;

namespace Trukman
{
	public class ConstructPage : ContentPage
	{
		public ConstructPage ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "This page is under construction" }
				}
			};
		}
	}
}


