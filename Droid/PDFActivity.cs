using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;

using System.IO;

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
                selectRect.Visibility = ViewStates.Visible;
                cancelButton.Visibility = ViewStates.Visible;
                doneButton.Visibility = ViewStates.Visible;
                scanButton.Visibility = ViewStates.Gone;
            }
            else
            {
                selectRect.Visibility = ViewStates.Invisible;
                cancelButton.Visibility = ViewStates.Gone;
                doneButton.Visibility = ViewStates.Gone;
                scanButton.Visibility = ViewStates.Visible;
            }
        }

		void ShowPdfDocument (Android.Net.Uri docUri)
		{
            pdfWebView = (WebView)FindViewById<WebView>(Resource.Id.PDFWebView);

            WebSettings settings = pdfWebView.Settings;
            settings.JavaScriptEnabled = true;
            settings.AllowUniversalAccessFromFileURLs = true;
            settings.AllowFileAccessFromFileURLs = true;

            settings.SetSupportZoom(true);

            pdfWebView.SetWebChromeClient(new WebChromeClient());


            string path = GetRealPathFromURI(docUri);
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

    public class JavaScriptResult : Java.Lang.Object, IValueCallback {
        public void OnReceiveValue(Java.Lang.Object result) {
            Java.Lang.String    json = (Java.Lang.String) result;
            // |json| is a string of JSON containing the result of your evaluation
        }
    }
}

