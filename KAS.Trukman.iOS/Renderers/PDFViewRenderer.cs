using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using KAS.Trukman.Controls;
using KAS.Trukman.iOS;
using System.IO;
using Foundation;
using System.Net;
using UIKit;

[assembly: ExportRenderer (typeof(PDFView), typeof(PDFViewRenderer))]
namespace KAS.Trukman.iOS
{
	#region PDFViewRenderer
	public class PDFViewRenderer : ViewRenderer<PDFView, UIWebView>
	{
		protected override void OnElementChanged (ElementChangedEventArgs<PDFView> args)
		{
			base.OnElementChanged (args);

			if (this.Control == null)
				this.SetNativeControl (new UIWebView ());

			if (args.NewElement != null) {
				var pdfView = (this.Element as PDFView);
				string fileName = Path.Combine (NSBundle.MainBundle.BundlePath, string.Format ("Content/{0}", WebUtility.UrlEncode (pdfView.Uri)));
				this.Control.LoadRequest (new NSUrlRequest (new NSUrl (fileName, false)));
				this.Control.ScalesPageToFit = true;
			}
		}

	}
	#endregion
}

