using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Trukman
{
	public class MenuList : ListView
	{

		public class ItemsMenu{
			public ItemsMenu(String title, string imageName, Type targetType){
				this.imageName = imageName;
				this.title = title;
				this.TargetType = targetType;
			}

			public string title{ private set;get;}
			public string imageName{ private set;get;}
			public ImageSource imageSource { get {return ImageSource.FromResource (imageName);} set { }}
			public Type TargetType{ get; set; }

		}

		public MenuList ()
		{

			var listItems = CreateListItems ();
			ItemsSource = listItems;

			BackgroundColor = Color.FromRgba (0, 0, 0, 0);


			ItemTemplate = new DataTemplate (() => {

				Label label = new Label { TextColor = Color.FromRgb (211, 211, 211) };
				label.SetBinding (Label.TextProperty, "title");

				Image image = new Image ();
				image.SetBinding (Image.SourceProperty, "imageSource");


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

		List<ItemsMenu> CreateListItems ()
		{
			// Строим меню согласно роли пользователя
			UserRole role = App.ServerManager.GetCurrentUserRole ();

			if (role == UserRole.UserRoleOwner) {
				return new List<ItemsMenu> {
					new ItemsMenu (Localization.getString (Localization.LocalStrings.MANAGE_DRIVERS), "pika.png", typeof(DriverListPage)),
					new ItemsMenu (Localization.getString (Localization.LocalStrings.MANAGE_DISPATCH), "pika.png", typeof(ConstructPage)),
					new ItemsMenu (Localization.getString (Localization.LocalStrings.MANAGE_JOBS), "pika.png", typeof(JobListPage)),
					new ItemsMenu (Localization.getString (Localization.LocalStrings.MANAGE_FLEET), "pika.png", typeof(ConstructPage)),
					new ItemsMenu (Localization.getString (Localization.LocalStrings.SETTING), "pika.png", typeof(ConstructPage)),
					new ItemsMenu (Localization.getString (Localization.LocalStrings.HELP), "pika.png", typeof(ConstructPage)),
					new ItemsMenu (Localization.getString (Localization.LocalStrings.CAMBIAR_A_ESPANOL), "pika.png", typeof(ConstructPage))
				};
			} else if (role == UserRole.UserRoleDispatch) {
				return new List<ItemsMenu> {
					new ItemsMenu (Localization.getString (Localization.LocalStrings.MANAGE_DRIVERS), "pika.png", typeof(DriverListPage)),
					new ItemsMenu (Localization.getString (Localization.LocalStrings.MANAGE_JOBS), "pika.png", typeof(JobListPage)),
					new ItemsMenu (Localization.getString (Localization.LocalStrings.HELP), "pika.png", typeof(ConstructPage)),
					new ItemsMenu (Localization.getString (Localization.LocalStrings.CAMBIAR_A_ESPANOL), "pika.png", typeof(ConstructPage))
				};
			}
			else
			{
				return new List<ItemsMenu> {
					new ItemsMenu (Localization.getString (Localization.LocalStrings.MANAGE_JOBS), "pika.png", typeof(DriverMainPage)),
					new ItemsMenu (Localization.getString (Localization.LocalStrings.HELP), "pika.png", typeof(ConstructPage)),
					new ItemsMenu (Localization.getString (Localization.LocalStrings.CAMBIAR_A_ESPANOL), "pika.png", typeof(ConstructPage))
				};
			}
		}
	}
}
