using System;

using Xamarin.Forms;

namespace Trukman
{
	public class RootDetailPage : ContentPage
	{
		public RootDetailPage ()
		{
			Title = "DetailPage";

			Content = new StackLayout {
				Children = {
					new Label {
						Text = "DetailPage",
						FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label))
					}
				}
			};
		}
	}
}


