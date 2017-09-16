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
using Android.Provider;
using KAS.Trukman.OCR;
using System.Text.RegularExpressions;
using Android.Content.Res;

namespace Trukman.Droid
{
    [Activity(Name = "com.trukman.ui.pdfactivity", Label = "PDFActivity", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class PDFActivity : Activity
    {
        private SelectionRectangleView selectRect;
        private WebView pdfWebView;
        private Button doneButton;
        private Button scanButton;
        private Button cancelButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PDF);

            doneButton = FindViewById<Button>(Resource.Id.doneButton);
            doneButton.Click += HandleModeSwitch;
            doneButton.Click += ScanImage;
            cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
            cancelButton.Click += HandleModeSwitch;
            scanButton = FindViewById<Button>(Resource.Id.scanButton);
            scanButton.Click += HandleModeSwitch;

            selectRect = FindViewById<SelectionRectangleView>(Resource.Id.PDFView);

            Android.Net.Uri uri = null;
            if (this.Intent.Action == Intent.ActionSend)
            {
                var bundle = this.Intent.Extras;
                uri = (Android.Net.Uri)bundle.Get(Intent.ExtraStream);
            }
            else
                uri = this.Intent.Data;
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

        //private string GetRealPathFromURI(Android.Net.Uri contentURI)
        //{
        //	var mediaStoreImagesMediaData = "_data";
        //	string[] projection = { mediaStoreImagesMediaData };

        //	Android.Database.ICursor cursor = ContentResolver.Query(contentURI, projection, 
        //		null, null, null);
        //	int columnIndex = cursor.GetColumnIndexOrThrow(mediaStoreImagesMediaData);
        //	cursor.MoveToFirst();

        //	string path = cursor.GetString(columnIndex);
        //	cursor.Close();
        //	return path;
        //}

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



            //Android.Database.ICursor cursor = null;
            //try
            //{
            //    String[] proj = { MediaStore.Images.Media.InterfaceConsts.Data };
            //    cursor = this.ContentResolver.Query(contentUri, proj, null, null, null);
            //    int column_index = cursor.GetColumnIndexOrThrow(MediaStore.Images.Media.InterfaceConsts.Data);
            //    cursor.MoveToFirst();
            //    String path = cursor.GetString(column_index);

            //    return path;
            //}
            //finally
            //{
            //    if (cursor != null)
            //    {
            //        cursor.Close();
            //    }
            //}
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
                            // TODO: заменить Alert
                            //AlertHandler.ShowAlert(response.ErrorMessage);
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
            string[] extraNames = new string[] { "Receiver", "Sender", "ReceiverAddress", "SenderAddress" };
            string[] items = new string[] { "Save as receiver", "Save as sender", "Save as receiver address", "Save as sender address" };
            var resultActivity = new Intent(this, typeof(OCRActivity));
            resultActivity.SetFlags(ActivityFlags.GrantReadUriPermission);
            AlertDialog.Builder alert = new AlertDialog.Builder(this);

            alert.SetTitle(result);
            alert.SetItems(items, (o, e) =>
            {
                resultActivity.PutExtra(extraNames[e.Which], result);
                StartActivity(resultActivity);
            });
            alert.SetNegativeButton("Cancel", delegate { });
            RunOnUiThread(() =>
            {
                alert.Show();
            });
        }
    }
}

