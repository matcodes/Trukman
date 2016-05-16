using System;

using Xamarin.Forms;
using KAS.Trukman.Storage.ParseClasses;

namespace KAS.Trukman
{
	public class ImageViewerPage : ContentPage
	{
		public ImageViewerPage (ParsePhoto photo)
		{
			this.Title = photo.Kind;

			var image = new Image
			{
				Source = ImageSource.FromUri(photo.Data.Url),
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


