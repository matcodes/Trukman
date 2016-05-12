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
	[Register ("RateConfirmationViewController")]
	partial class RateConfirmationViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton jobButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView logo { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton sendButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton signButton { get; set; }

		[Action ("createJobPressed:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void createJobPressed (UIButton sender);

		[Action ("sendPressed:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void sendPressed (UIButton sender);

		[Action ("signPressed:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void signPressed (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (jobButton != null) {
				jobButton.Dispose ();
				jobButton = null;
			}
			if (logo != null) {
				logo.Dispose ();
				logo = null;
			}
			if (sendButton != null) {
				sendButton.Dispose ();
				sendButton = null;
			}
			if (signButton != null) {
				signButton.Dispose ();
				signButton = null;
			}
		}
	}
}
