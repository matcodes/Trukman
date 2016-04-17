using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Webkit;

using Newtonsoft.Json;
using Trukman.Droid.Views;
using KAS.Trukman.Droid;

namespace Trukman.Droid
{
	[Activity (Name="com.trukman.ui.pdfactivity", Label = "PDFActivity", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
	public class PDFActivity : Activity
	{
		private SelectionRectangleView selectRect;
		private WebView pdfWebView;
		private Button doneButton;
		private Button scanButton;
		private Button cancelButton;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView(Resource.Layout.PDF);

			doneButton = FindViewById<Button>(Resource.Id.doneButton);
			doneButton.Click += HandleModeSwitch;
			doneButton.Click += ScanImage;
			cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
			cancelButton.Click += HandleModeSwitch;
			scanButton = FindViewById<Button>(Resource.Id.scanButton);
			scanButton.Click += HandleModeSwitch;

			selectRect = FindViewById<SelectionRectangleView>(Resource.Id.PDFView);

			ShowPdfDocument (Intent.Data);
		}

		protected override void OnResume ()
		{
			base.OnResume();
			pdfWebView.LoadUrl("javascript:window.location.reload( true )");
		}

		protected override void OnPause ()
		{
			base.OnPause();
			pdfWebView.ClearCache(true);
		}

		// Switch to scan/view mode
		void HandleModeSwitch(object sender, EventArgs ea) {
			if (selectRect.Visibility == ViewStates.Invisible)
			{
				RunOnUiThread(() =>
					{
						selectRect.Visibility = ViewStates.Visible;
						cancelButton.Visibility = ViewStates.Visible;
						doneButton.Visibility = ViewStates.Visible;
						scanButton.Visibility = ViewStates.Gone;
					});
			}
			else
			{
				RunOnUiThread(() =>
					{
						selectRect.Visibility = ViewStates.Invisible;
						cancelButton.Visibility = ViewStates.Gone;
						doneButton.Visibility = ViewStates.Gone;
						scanButton.Visibility = ViewStates.Visible;
					});
			}
		}

		void ShowPdfDocument (Android.Net.Uri docUri)
		{
			pdfWebView = FindViewById<WebView>(Resource.Id.PDFWebView);

			WebSettings settings = pdfWebView.Settings;
			settings.JavaScriptEnabled = true;
			settings.AllowUniversalAccessFromFileURLs = true;
			settings.AllowFileAccessFromFileURLs = true;
			settings.AllowContentAccess = true; // Check this!

			pdfWebView.SetWebChromeClient(new WebChromeClient());

			string path = docUri.Path;
			if (docUri.Scheme == "content")
			{
				path = GetRealPathFromURI(docUri);
			}

			pdfWebView.LoadUrl("file:///android_asset/pdfjs/web/viewer.html?file=" + WebUtility.UrlEncode(path));
		}

		private string GetRealPathFromURI(Android.Net.Uri contentURI)
		{
			var mediaStoreImagesMediaData = "_data";
			string[] projection = { mediaStoreImagesMediaData };

			Android.Database.ICursor cursor = ContentResolver.Query(contentURI, projection, 
				null, null, null);
			int columnIndex = cursor.GetColumnIndexOrThrow(mediaStoreImagesMediaData);
			cursor.MoveToFirst();

			string path = cursor.GetString(columnIndex);
			cursor.Close();
			return path;
		}

		void ScanImage (object sender, EventArgs ea)
		{
			Rect bounds = selectRect.getBounds();
			pdfWebView.DrawingCacheEnabled = true;
			Bitmap selectedFragment = Bitmap.CreateBitmap(pdfWebView.GetDrawingCache(true), bounds.Left, bounds.Top, bounds.Width(), bounds.Height());
			pdfWebView.DrawingCacheEnabled = false;

			OCRApi ocr = new OCRApi();

			ocr.Parse(selectedFragment)
				.ContinueWith((task) =>
					{
						OCRResponse response = task.Result;
						if (response.OCRExitCode == 1)
						{
							ShowResult(response.ParsedResults[0].ParsedText);
						}
						else
						{
							// TODO: заменить Alert
							//AlertHandler.ShowAlert(response.ErrorMessage);
						}
					});
		}

		void ShowResult(string result)
		{
			string[] extraNames = new string[]{"Receiver", "Sender", "ReceiverAddress", "SenderAddress"};
			string[] items = new string[]{"Save as receiver", "Save as sender", "Save as receiver address", "Save as sender address"};
			var resultActivity = new Intent(this, typeof(OCRActivity));
			AlertDialog.Builder alert = new AlertDialog.Builder(this);

			alert.SetTitle(result);
			alert.SetItems(items, (o, e) => {
				resultActivity.PutExtra(extraNames[e.Which], result);
				StartActivity(resultActivity);
			});
			alert.SetNegativeButton("Cancel", delegate {});
			RunOnUiThread(() =>
				{
					alert.Show();
				});

		}
	}
}

