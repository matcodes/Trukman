using System;

using Xamarin.Forms;
using KAS.Trukman.Storage.ParseClasses;
using KAS.Trukman.Data.Classes;

namespace KAS.Trukman
{
	public class ImageViewerPage : ContentPage
	{
		public ImageViewerPage (Photo photo)
		{
			this.Title = photo.Type;

			var image = new Image
			{
				Source = ImageSource.FromUri(photo.Uri),
				Aspect = Aspect.AspectFit,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			Content = new ScrollView
			{
				Padding = 20,
				Content = new StackLayout
				{
					Spacing = 10,
					VerticalOptions = LayoutOptions.FillAndExpand,
					Children = {image}
				}
			};

		}
	}
}


