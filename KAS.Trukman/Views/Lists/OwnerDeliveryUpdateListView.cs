using System;

using Xamarin.Forms;

namespace KAS.Trukman.Views.Lists
{
	public class OwnerDeliveryUpdateListView : BaseListView
	{
		public OwnerDeliveryUpdateListView()
			: base() {
			this.BackgroundColor = Color.Transparent;
			this.IsPullToRefreshEnabled = true;
			this.IsGroupingEnabled = true;
			this.GroupDisplayBinding = new Binding ("Key");
			//this.GroupShortNameBinding = new Binding ("Key");
			if(Device.RuntimePlatform != Device.WinPhone)
				this.GroupHeaderTemplate = new DataTemplate(typeof(HeaderCell));

			var cell = new DataTemplate(typeof(DeliveryUpdateCell));

			cell.SetBinding(TextCell.TextProperty, "Type");
			cell.SetBinding(TextCell.DetailProperty, "Job.Driver.FullName");

			this.ItemTemplate = cell;

		}
	}

	public class DeliveryUpdateCell : TextCell
	{
		public DeliveryUpdateCell()
		{
			this.TextColor = Color.White;
			this.DetailColor = Color.White;
		}
	}

	public class HeaderCell : ViewCell
	{
		public HeaderCell()
		{
			this.Height = 25;
			var title = new Label {
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.Center
			};

			title.SetBinding(Label.TextProperty, "Key");

			View = new StackLayout
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				HeightRequest = 25,
				BackgroundColor =  new Color (1, 0, 0, 0.5),
				Padding = 5,
				Orientation = StackOrientation.Horizontal,
				Children = { title }
			};
		}
	}
}


