// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace KAS.Trukman.iOS
{
	[Register ("CarInfoViewController")]
	partial class CarInfoViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel distanceLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel timeLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (distanceLabel != null) {
				distanceLabel.Dispose ();
				distanceLabel = null;
			}
			if (timeLabel != null) {
				timeLabel.Dispose ();
				timeLabel = null;
			}
		}
	}
}
