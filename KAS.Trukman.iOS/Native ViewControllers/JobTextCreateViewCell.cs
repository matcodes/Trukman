using System;

using Foundation;
using UIKit;

namespace KAS.Trukman.iOS
{
	public partial class JobTextCreateViewCell : UITableViewCell
	{
		[Outlet]
		public UITextField textField { get; set; }
		[Outlet]
		public UILabel _textLabel { get; set; }

		public static readonly NSString Key = new NSString ("JobTextCreateViewCell");
		public static readonly UINib Nib;

		static JobTextCreateViewCell ()
		{
			Nib = UINib.FromName ("JobTextCreateViewCell", NSBundle.MainBundle);
		}

		public JobTextCreateViewCell (IntPtr handle) : base (handle)
		{
		}
	}
}
