using System;
using KAS.Trukman.Views.Lists;
using Xamarin.Forms;

namespace KAS.Trukman
{
	#region BrokerListView
	public class BrokerListView : BaseListView
	{
		public BrokerListView ()
			: base()
		{
			this.BackgroundColor = Color.Transparent;

			this.IsPullToRefreshEnabled = true;

			this.ItemTemplate = new DataTemplate(typeof(BrokerCell));
		}
	}
	#endregion

	#region BrokerCell
	public class BrokerCell : TextCell
	{
		public BrokerCell() 
			: base()
		{
			this.SetBinding(BrokerCell.TextProperty, "FullName");
			this.TextColor = Color.White;
		}
	}
	#endregion
}

