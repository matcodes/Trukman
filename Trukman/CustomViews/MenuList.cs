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
			public ImageSource imageSource { 
				get {
					return (!string.IsNullOrEmpty (imageName) ? imageName : null);
					/*if (!string.IsNullOrEmpty (imageName))
						return ImageSource.FromFile (imageName);
					else
						return null;*/
				} 
				set { }
			}
			public Type TargetType{ get; set; }

		}

		public MenuList ()
		{
			var listItems = CreateListItems ();
			this.SeparatorVisibility = SeparatorVisibility.Default;
			//this.IsPullToRefreshEnabled = true;
			ItemsSource = listItems;

			BackgroundColor = Color.FromRgba (0, 0, 0, 0);


			ItemTemplate = new DataTemplate (() => {

				Label label = new Label { TextColor = Color.FromRgb (211, 211, 211) };
				label.SetBinding (Label.TextProperty, "title");

				Image image = new Image ();
				image.HorizontalOptions = LayoutOptions.End;
				image.VerticalOptions = LayoutOptions.Center;
				image.SetBinding (Image.SourceProperty, "imageSource");

				return new ViewCell {
					View = new StackLayout {
						BackgroundColor = Color.Transparent,
						Padding = new Thickness (0, 0),
						Orientation = StackOrientation.Horizontal,
						Children = {
							new StackLayout {
								VerticalOptions = LayoutOptions.Center,
								HorizontalOptions = LayoutOptions.FillAndExpand,
								Spacing = 0,
								Children = {
									label
								}
							},
							image
						}

					}
				};
			});

		}

		List<ItemsMenu> CreateListItems ()
		{
			UserRole role = App.ServerManager.GetCurrentUserRole ();

			if (role == UserRole.UserRoleOwner) {
				return new List<ItemsMenu> { 
					new ItemsMenu ("Rate Confirmation", PlatformHelper.ArrowImageSource, null),
					new ItemsMenu ("Dispatch Driver", PlatformHelper.ArrowImageSource, null),
					new ItemsMenu ("Load Confirmation", PlatformHelper.ArrowImageSource, null),
					new ItemsMenu ("Broker List", PlatformHelper.ArrowImageSource, null),
					new ItemsMenu ("Fuel Advance", PlatformHelper.ArrowImageSource, typeof(LumperPage)),
					new ItemsMenu ("Track Fleet", PlatformHelper.ArrowImageSource, null),
					new ItemsMenu ("Lumper", PlatformHelper.ArrowImageSource, null),
					new ItemsMenu ("Invoice", PlatformHelper.ArrowImageSource, null),
					new ItemsMenu ("Reports", PlatformHelper.ArrowImageSource, null),
					new ItemsMenu ("Delay Alerts", PlatformHelper.ArrowImageSource, null),
					new ItemsMenu ("Delivery Update", PlatformHelper.ArrowImageSource, null)
				};
			} else if (role == UserRole.UserRoleDispatch) {
				return new List<ItemsMenu>{ };
			} 
			else {
				return new List<ItemsMenu> {
					new ItemsMenu ("Return to current", PlatformHelper.ArrowImageSource, null),
					new ItemsMenu ("Shipper", PlatformHelper.ArrowImageSource, null),
					new ItemsMenu ("Receiver", PlatformHelper.ArrowImageSource, null),
					new ItemsMenu ("Fuel Advance", PlatformHelper.ArrowImageSource, null),
					new ItemsMenu ("Lumper", PlatformHelper.ArrowImageSource, typeof(LumperPage)),
					new ItemsMenu ("Delay/Emergency", PlatformHelper.ArrowImageSource, null),
					new ItemsMenu ("Route", PlatformHelper.ArrowImageSource, null),
					new ItemsMenu ("Points and LeaderBoard", PlatformHelper.ArrowImageSource, null)
				};
			}

			/*
			// Строим меню согласно роли пользователя
			if (role == UserRole.UserRoleOwner) {
				return new List<ItemsMenu> {
					new ItemsMenu (Localization.getString (Localization.LocalStrings.MANAGE_DRIVERS), "pika.png", typeof(DriverListPage)),
					new ItemsMenu (Localization.getString (Localization.LocalStrings.MANAGE_DISPATCH), "pika.png", typeof(DispatchListPage)),
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
			} else {
				return new List<ItemsMenu> {
					new ItemsMenu (Localization.getString (Localization.LocalStrings.MANAGE_JOBS), "pika.png", typeof(DriverMainPage)),
					new ItemsMenu (Localization.getString (Localization.LocalStrings.HELP), "pika.png", typeof(ConstructPage)),
					new ItemsMenu (Localization.getString (Localization.LocalStrings.CAMBIAR_A_ESPANOL), "pika.png", typeof(ConstructPage))
				};
			}*/
		}
	}
}
