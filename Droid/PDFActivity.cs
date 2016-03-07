using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PSPDFKit.Configuration.Theming;
using PSPDFKit.Configuration.Activity;
using PSPDFKit.Configuration.Page;
using PSPDFKit;
using PSPDFKit.UI;

using PSPDFKit.Document;
using PSPDFKit.Document.Library;
using System.IO;

namespace Trukman.Droid
{
	[Activity (Name="com.trukman.ui.pdfactivity", Label = "PDFActivity", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
	public class PDFActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Активизация просмотрщика PDF
			PSPDFKitGlobal.Initialize (this, GetString(Resource.String.pspdfkitLicenseKey));

			// Create your application here
			ShowPdfDocument (Intent.Data);
		}

		void SearchText()
		{
			//PSPDFDocument
			//string _path = System.Environment.GetFolderPath(System.Environment.CurrentDirectory);
			//PSPDFLibrary library = PSPDFLibrary.Get (Path.Combine (_path, "library.db"));
		}

		void ShowPdfDocument (Android.Net.Uri docUri)
		{
			// Customize thumbnailBar color defaults
			var thumbnailBarThemeConfiguration = new ThumbnailBarThemeConfiguration.Builder (this)
				.SetBackgroundColor (Android.Graphics.Color.Argb (255, 52, 152, 219))
				.SetThumbnailBorderColor (Android.Graphics.Color.Argb (255, 44, 62, 80))
				.Build ();

			// Show Document using PSPDFKit activity
			var pspdfkitConfiguration = new PSPDFActivityConfiguration.Builder (ApplicationContext, GetString(Resource.String.pspdfkitLicenseKey))
				.ScrollDirection (PageScrollDirection.Horizontal)
				.ShowPageNumberOverlay ()
				.ShowThumbnailGrid ()
				.ShowThumbnailBar ()
				.ThumbnailBarThemeConfiguration (thumbnailBarThemeConfiguration)
				.Build ();

			if (!PSPDFKitGlobal.IsOpenableUri (this, docUri)) {
				ShowError ("This document uri cannot be opened \n " + docUri.ToString ());
			}
			else
				PSPDFActivity.ShowDocument (this, docUri, pspdfkitConfiguration);
		}

		void ShowError (string message = null)
		{
			var alert = new AlertDialog.Builder (this);

			alert.SetTitle ("Error");
			alert.SetMessage (message ?? "There was an error");

			alert.SetPositiveButton ("Ok", (senderAlert, args) => {
				// Do something here to handle error
			});

			if (message != null) {
				alert.SetNeutralButton ("Visit", (sender, e) => {
					var uri = Android.Net.Uri.Parse ("https://pspdfkit.com/android/");
					var intent = new Intent (Intent.ActionView, uri);
					StartActivity (intent);
				});
			}

			RunOnUiThread (() => {
				alert.Show();
			});
		}
	}
}

