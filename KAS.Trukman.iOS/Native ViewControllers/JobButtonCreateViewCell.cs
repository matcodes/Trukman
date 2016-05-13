using System;

using Foundation;
using UIKit;

namespace KAS.Trukman.iOS
{
	public partial class JobButtonCreateViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("JobButtonCreateViewCell");
		public static readonly UINib Nib;
		[Outlet]
		public UIButton actionButton { get; set; }
		[Outlet]
		public UILabel _textLabel { get; set; }


		static JobButtonCreateViewCell ()
		{
			Nib = UINib.FromName ("JobButtonCreateViewCell", NSBundle.MainBundle);
		}

		public JobButtonCreateViewCell (IntPtr handle) : base (handle)
		{
		}
	}
}
