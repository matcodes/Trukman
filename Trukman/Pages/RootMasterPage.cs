using Xamarin.Forms;
using System.Collections.Generic;

namespace Trukman
{
	public class RootMasterPage : ContentPage
	{
		public ListView ListView { get { return listView; } }

		ListView listView;

		public RootMasterPage ()
		{
			//NavigationPage.SetHasNavigationBar (this, false);

			var masterPageItems = new List<MasterPageItem> ();

			if (App.ServerManager.GetCurrentUserRole () == UserRole.UserRoleDispatch) {
				masterPageItems.Add (new MasterPageItem {
					Title = "Drivers",
					//IconSource = "icon.png",
					TargetType = typeof(DriverListPage)
				});
			}

			masterPageItems.Add (new MasterPageItem {
				Title = "Jobs",
				//IconSource = "icon.png",
				TargetType = typeof(JobListPage)
			});

			listView = new ListView {
				ItemsSource = masterPageItems,
				ItemTemplate = new DataTemplate (() => {
					var imageCell = new ImageCell ();
					imageCell.SetBinding (TextCell.TextProperty, "Title");
					//imageCell.SetBinding (ImageCell.ImageSourceProperty, "IconSource");
					return imageCell;
				}),
				VerticalOptions = LayoutOptions.FillAndExpand,
				SeparatorVisibility = SeparatorVisibility.None
			};

			Padding = new Thickness (0, 40, 0, 0);
			Icon = "hamburger.png";
			Title = "Company Task";
			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					listView
				}	
			};
		}
	}
}

/*
		private ListView listView;

		public ListView ListView { get { return listView; } }

		public RootMasterPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);

			RelativeLayout relativeLayout = new RelativeLayout{VerticalOptions = LayoutOptions.Fill};
			RelativeLayout relativeLayout2 = new RelativeLayout{ VerticalOptions = LayoutOptions.Fill };

			Image image = new Image{Source = ImageSource.FromResource("pika.png"), Aspect = Aspect.AspectFit};

			Label labelNameUser = new Label{Text = "User Name", TextColor = Color.FromRgb(211,211,211)};

			Label labelBred = new Label { Text = "o/o", TextColor = Color.FromRgb (164,164,164) };

			Label labelMenu = new Label{ Text =  "Menu" , TextColor = Color.FromRgb (211, 211, 211) };

			MenuList menu = new MenuList ();

			BoxView box = new BoxView{ BackgroundColor = Color.FromRgb (43, 43, 43),WidthRequest = 500, HeightRequest = 20 };


			var masterPageItems = new List<MasterPageItem> ();
			masterPageItems.Add (new MasterPageItem{ Title = "Drivers", TargetType = typeof(DriversPage) });
			masterPageItems.Add (new MasterPageItem{ Title = "Jobs", TargetType = typeof(JobsPage) });
			//listView.ItemsSource = masterPageItems;

			listView = new ListView {
				ItemsSource = masterPageItems,
				ItemTemplate = new DataTemplate (() => {
					var imageCell = new ImageCell ();
					imageCell.SetBinding (TextCell.TextProperty, "Title");
					//imageCell.SetBinding (ImageCell.ImageSourceProperty, "IconSource");
					return imageCell;
				}),
				VerticalOptions = LayoutOptions.FillAndExpand,
				SeparatorVisibility = SeparatorVisibility.None
			};


			/*relativeLayout.Children.Add (image, Constraint.RelativeToParent ((Parent) => {
				return Parent.Width / 20;
			}),
				Constraint.RelativeToParent ((Parent) => {
					return 20;
				}),
				Constraint.RelativeToParent ((Parent) => {
					return Parent.Width / 5;
				}));

			relativeLayout.Children.Add (labelNameUser, Constraint.RelativeToView(image, (parent, view) =>{
				return view.X + view.Width + parent.Width / 50;
			}),
				Constraint.RelativeToView(image, (parent, view) =>{
					return view.Y;
				}));

			relativeLayout.Children.Add (labelBred, Constraint.RelativeToView (labelNameUser, (parent, view) => {
				return view.X;
			}),
				Constraint.RelativeToView(labelNameUser, (parent, view) =>{
					return view.Y + parent.Width / 20;		
				}));

			relativeLayout2.Children.Add (box, Constraint.RelativeToParent ((Parent) => {
				return 2;
			}),
				Constraint.RelativeToParent ((Parent) => {
					return 1;
				}));

			relativeLayout2.Children.Add (labelMenu, Constraint.RelativeToView (box, (parent, view) => {
				return view.X + 30;
			}),
				Constraint.RelativeToView (box, (parent, view) => {
					return view.Y;
				}));

			relativeLayout2.Children.Add (menu, Constraint.RelativeToView (box, (parent, view) => {
				return view.X + 5;
			}),
				Constraint.RelativeToView(box, (parent, view) => {
					return view.Y + 25;	
				}));

			Padding = new Thickness (0, 40, 0, 0);
			Title = "MasterPage";
			BackgroundColor = Color.FromRgb (126, 126, 126);

			Content = new StackLayout { 
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					listView/*,
					relativeLayout,
					new SearchBar{Placeholder = "Search", BackgroundColor = Color.FromRgb(126,126,126)},
					relativeLayout2
				}
			};
		}
	}
}

*/
