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
	[Register ("OCRResultViewController")]
	partial class OCRResultViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView scannedTextView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		NSLayoutConstraint textView { get; set; }

		[Action ("cancelPressed:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void cancelPressed (UIBarButtonItem sender);

		[Action ("savePressed:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void savePressed (UIBarButtonItem sender);

		void ReleaseDesignerOutlets ()
		{
			if (scannedTextView != null) {
				scannedTextView.Dispose ();
				scannedTextView = null;
			}
			if (textView != null) {
				textView.Dispose ();
				textView = null;
			}
		}
	}
}
