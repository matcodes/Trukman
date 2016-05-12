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
	[Register ("OCRPdfViewController")]
	partial class OCRPdfViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem highlightButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem scanButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIWebView webView { get; set; }

		[Action ("highlightPressed:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void highlightPressed (UIBarButtonItem sender);

		[Action ("scanPressed:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void scanPressed (UIBarButtonItem sender);

		void ReleaseDesignerOutlets ()
		{
			if (highlightButton != null) {
				highlightButton.Dispose ();
				highlightButton = null;
			}
			if (scanButton != null) {
				scanButton.Dispose ();
				scanButton = null;
			}
			if (webView != null) {
				webView.Dispose ();
				webView = null;
			}
		}
	}
}
