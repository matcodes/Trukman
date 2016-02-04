using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Trukman
{
	public class MenuList : ListView
	{

		public class ItemsMenu{
			public ItemsMenu(String title, string imageName){
				this.imageName = imageName;
				this.title = title;
			}

			public string title{ private set;get;}
			public string imageName{ private set;get;}
			public ImageSource imageSource { get {return ImageSource.FromResource (imageName);} set { }}

		}

		public MenuList ()
		{
			
			List<ItemsMenu> listItems = new List<ItemsMenu>
			{
				new ItemsMenu("Manage Drivers", "pika.png"),

				new ItemsMenu("Manage Dispatch", "pika.png"),

				new ItemsMenu("Manage Fleet", "pika.png"),

				new ItemsMenu("Setting", "pika.png"),

				new ItemsMenu("Help", "pika.png"),

				new ItemsMenu("Cambiar a Espanol","pika.png")
					};
			ItemsSource = listItems;

			BackgroundColor = Color.FromRgba (0, 0, 0, 0);


			ItemTemplate = new DataTemplate (() => {

				Label label = new Label {TextColor = Color.FromRgb(211,211,211)};
				label.SetBinding (Label.TextProperty, "title");

				Image image = new Image ();
				image.SetBinding(Image.SourceProperty, "imageSource");


				return new ViewCell {
					View = new StackLayout {
//						BackgroundColor = Color.FromRgba(0, 0, 0, 0),
						Padding = new Thickness (0, 0),
						Orientation = StackOrientation.Horizontal,
						Children = {
							image,
							new StackLayout {
								VerticalOptions = LayoutOptions.Center,
								Spacing = 0,
								Children = {
									label
								}
							}
						}

					}
				};
			});

		}


	}
}

