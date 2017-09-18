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
using Trukman.Droid.Views;
using Android.Webkit;
using System.Net;
using Android.Graphics;
using System.IO;
using KAS.Trukman.OCR;
using System.Text.RegularExpressions;

namespace KAS.Trukman.Droid.Activities
{
    [Activity(Name = "com.trukman.ui.ocrpdfactivity", Label = "OCRPdfActivity", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class OCRPdfActivity : Activity
    {
        private SelectionRectangleView selectRect;
        private WebView pdfWebView;
        private Button doneButton;
        private Button scanButton;
        private Button cancelButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.OCRPdf);

            doneButton = FindViewById<Button>(Resource.Id.doneButton);
            doneButton.Click += HandleModeSwitch;
            doneButton.Click += ScanImage;
            cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
            cancelButton.Click += HandleModeSwitch;
            scanButton = FindViewById<Button>(Resource.Id.scanButton);
            scanButton.Click += HandleModeSwitch;

            selectRect = FindViewById<SelectionRectangleView>(Resource.Id.PDFView);

            Android.Net.Uri uri = this.Intent.Data;
            if (uri != null)
                ShowPdfDocument(uri);
        }

        protected override void OnResume()
        {
            base.OnResume();
            //pdfWebView.LoadUrl("javascript:window.location.reload( true )");
        }

        protected override void OnPause()
        {
            base.OnPause();
            //pdfWebView.ClearCache(true);
        }

        // Switch to scan/view mode
        void HandleModeSwitch(object sender, EventArgs ea)
        {
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

        void ShowPdfDocument(Android.Net.Uri docUri)
        {
            pdfWebView = FindViewById<WebView>(Resource.Id.PDFWebView);

            WebSettings settings = pdfWebView.Settings;
            settings.JavaScriptEnabled = true;
            settings.AllowUniversalAccessFromFileURLs = true;
            settings.AllowFileAccessFromFileURLs = true;
            settings.AllowContentAccess = true; // Check this!
            settings.BuiltInZoomControls = true;
            pdfWebView.SetWebChromeClient(new WebChromeClient());

            string path = docUri.Path;
            if (docUri.Scheme == "content")
            {
                path = this.GetRealPathFromURI(docUri);
            }

            pdfWebView.LoadUrl("file:///android_asset/pdfjs/web/viewer.html?file=" + WebUtility.UrlEncode(path));
        }

        private String GetRealPathFromURI(Android.Net.Uri contentUri)
        {
            var fileName = "";
            try
            {
                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                fileName = System.IO.Path.Combine(path, "current.pdf");
                using (var stream = this.ContentResolver.OpenInputStream(contentUri))
                using (var fileStream = new FileStream(fileName, FileMode.Create))
                    stream.CopyTo(fileStream);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return fileName;
        }

        void ScanImage(object sender, EventArgs ea)
        {
            Rect bounds = selectRect.getBounds();
            pdfWebView.DrawingCacheEnabled = true;
            Bitmap selectedFragment = Bitmap.CreateBitmap(pdfWebView.GetDrawingCache(true), bounds.Left, bounds.Top, bounds.Width(), bounds.Height());
            pdfWebView.DrawingCacheEnabled = false;

            OCRApi ocr = new OCRApi();

            ocr.Parse(BitmapToByteArray(selectedFragment))
                .ContinueWith((task) =>
                {
                    OCRResponse response = task.Result;
                    if (response.OCRExitCode == 1)
                    {
                        string parsedText = response.ParsedResults[0].ParsedText.Replace("\\r\\n", "").Replace("\r\n", "");
                        Regex regEx = new Regex(@"\s+");
                        parsedText = regEx.Replace(parsedText, " ");

                        ShowResult(parsedText);
                    }
                    else
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(this);
                        alert.SetTitle("Error");
                        alert.SetMessage(response.ErrorMessage);
                        alert.SetPositiveButton("Ok", (senderAlert, args) => { });
                        Dialog dialog = alert.Create();
                        dialog.Show();
                    }
                });
        }

        private static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            byte[] bitmapData;
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 0, stream);
                bitmapData = stream.ToArray();
            }

            return bitmapData;
        }

        void ShowResult(string result)
        {
            string[] extraNames = new string[]
            {
                "JobNumber",
                "Weight",
                "ShipperName",
                "LoadFrom",
                "PickupTime",
                "ReceiverName",
                "Destination",
                "DropTime",
                "Price"
            };

            string[] items = new string[]
            {
                "Job number",
                "Weight",
                "Shipper name",
                "Load from",
                "Pickup time",
                "Receiver name",
                "Destination",
                "Drop time",
                "Price"
            };

            var resultIntent = new Intent(this, typeof(OCRResultActivity));
            resultIntent.SetFlags(ActivityFlags.GrantReadUriPermission);
            AlertDialog.Builder alert = new AlertDialog.Builder(this);

            alert.SetTitle(result);
            alert.SetItems(items, (o, e) =>
            {
                resultIntent.PutExtra(extraNames[e.Which], result);
                this.StartActivity(resultIntent);
            });
            alert.SetNegativeButton("Cancel", delegate { });
            RunOnUiThread(() =>
            {
                alert.Show();
            });
        }
    }
}

