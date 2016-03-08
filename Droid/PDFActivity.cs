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

using System.IO;

namespace Trukman.Droid
{
	[Activity (Name="com.trukman.ui.pdfactivity", Label = "PDFActivity", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
	public class PDFActivity : Activity
	{
        private DragRect dr;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

            SetContentView(Resource.Layout.PDF);
            dr = FindViewById<DragRect>(Resource.Id.PDFView);

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

