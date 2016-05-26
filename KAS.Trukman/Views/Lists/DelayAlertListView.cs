using System;
using KAS.Trukman.Views.Lists;
using Xamarin.Forms;
using KAS.Trukman.Languages;

namespace KAS.Trukman
{
	#region DelayAlertListView
	public class DelayAlertListView : BaseListView
	{
		public DelayAlertListView ()
		{
			this.BackgroundColor = Color.Transparent;
			this.IsPullToRefreshEnabled = true;
			this.IsGroupingEnabled = true;
			this.GroupDisplayBinding = new Binding ("Job");
			if(Device.OS != TargetPlatform.WinPhone)
				this.GroupHeaderTemplate = new DataTemplate(typeof(DelayAlertGroupCell));
			this.ItemTemplate = new DataTemplate (typeof(DelayAlertItemCell));

		}
	}
	#endregion

	#region DelayAlertGroupCell
	public class DelayAlertGroupCell : ViewCell
	{
		public DelayAlertGroupCell()
			: base()
		{
			var jobLabel = new Label
			{
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Start,
				TextColor = Color.Black,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
			};
			jobLabel.SetBinding(Label.TextProperty, new Binding("JobAlertListJobNumberLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

			var job = new Label
			{
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Start,
				TextColor = Color.Black,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				FontAttributes = FontAttributes.Bold
			};
			job.SetBinding(Label.TextProperty, "Job.JobRef", BindingMode.OneWay);

			var driverLabel = new Label
			{
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Start,
				TextColor = Color.Black,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
			};
			driverLabel.SetBinding(Label.TextProperty, new Binding("JobAlertListDriverNameLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

			var driver = new Label
			{
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Start,
				TextColor = Color.Black,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				FontAttributes = FontAttributes.Bold
			};
			driver.SetBinding(Label.TextProperty, "Job.Driver.FullName", BindingMode.OneWay);

			var grid = new Grid
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				ColumnSpacing = 10,
				RowSpacing = 0,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
				},
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
				}
			};
			grid.Children.Add(jobLabel, 0, 0);
			grid.Children.Add(job, 1, 0);
			grid.Children.Add(driverLabel, 0, 1);
			grid.Children.Add(driver, 1, 1);

			var displayNameContent = new ContentView
			{
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.Fill,
				Padding = new Thickness(10, 0, 10, 0),
				Content = grid
			};

			this.View = displayNameContent;
        }
	}
	#endregion

	#region DelayAlertItemCell
	public class DelayAlertItemCell : TextCell
	{
		public DelayAlertItemCell() 
			: base()
		{
			var delayEmergenceToTextConverter = new DelayEmergenceToTextConverter ();

			this.TextColor = Color.White;
			this.DetailColor = Color.White;

			this.SetBinding (TextCell.TextProperty, new Binding("AlertType", BindingMode.OneWay, delayEmergenceToTextConverter));
			this.SetBinding (TextCell.DetailProperty, "AlertText");
		}
	}
	#endregion
}

