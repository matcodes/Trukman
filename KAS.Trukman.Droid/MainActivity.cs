﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using KAS.Trukman.Helpers;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using Android.Content;
using Trukman.Helpers;
using Trukman.Droid.Helpers;
using Android.Provider;
using Android.Media;
using Android.Graphics;
using System.IO;
using System.Threading.Tasks;
using KAS.Trukman.Data.Interfaces;

namespace KAS.Trukman.Droid
{
    #region MainActivity
    [Activity (Label = "TRUKMAN", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
        #region Static members
        public static readonly int TAKE_PHOTO_REQUEST_CODE = 1;
        #endregion

        private ITrip _trip = null;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            var platformHelper = new AndroidPlatformHelper(this);

            PlatformHelper.Initialize(platformHelper);
			global::Xamarin.Forms.Forms.Init (this, bundle);
            Xamarin.Forms.Forms.SetTitleBarVisibility(Xamarin.Forms.AndroidTitleBarVisibility.Never);

			SettingsServiceHelper.Initialize (new SettingsService ());
            Xamarin.FormsMaps.Init(this, bundle);

			LoadApplication (new KAS.Trukman.App ());
		}

        protected override void OnResume()
        {
            base.OnResume();

            ShowToastMessage.Subscribe(this, this.ShowToast);
            ShowGPSSettingsMessage.Subscribe(this, this.ShowGPSSettings);
            TakePhotoFromCameraMessage.Subscribe(this, this.TakePhotoFromCamera);
        }

        protected override void OnPause()
        {
            ShowToastMessage.Unsubscribe(this);
            ShowGPSSettingsMessage.Unsubscribe(this);
            TakePhotoFromCameraMessage.Unsubscribe(this);

            base.OnPause();
        }

        private void ShowToast(ShowToastMessage message)
        {
            this.RunOnUiThread(() => {
                Toast.MakeText(this, message.Text, ToastLength.Long).Show();
            });
        }

        private void ShowGPSSettings(ShowGPSSettingsMessage message)
        {
            Intent callGPSSettingIntent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
            this.StartActivity(callGPSSettingIntent);
        }

        private void TakePhotoFromCamera(TakePhotoFromCameraMessage message)
        {
            if (message.Trip != null)
            {
                _trip = message.Trip;
                Intent intent = new Intent(MediaStore.ActionImageCapture);
                StartActivityForResult(intent, TAKE_PHOTO_REQUEST_CODE);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                if (requestCode == TAKE_PHOTO_REQUEST_CODE)
                {
                    var result = data.Data.ToString();
                    this.SavePhotoToStore(result);
                    ShowAdvancesPageMessage.Send(_trip);
                }
            }
        }

        private void SavePhotoToStore(string uri)
        {
            Task.Run(async () =>
            {
                try
                {
                    var data = new byte[] { };
                    var bitmap = MediaStore.Images.Media.GetBitmap(this.ContentResolver, Android.Net.Uri.Parse(uri));

                    var rotate = this.GetRotation(uri);

                    if (rotate > 0)
                    {
                        Matrix matrix = new Matrix();
                        matrix.PostRotate(rotate);
                        var rotatedBitmap = Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);
                        bitmap.Recycle();
                        bitmap = rotatedBitmap;
                    }

                    var val = (bitmap.Width > bitmap.Height ? bitmap.Width : bitmap.Height);
                    var percent = (960.0f / val) * 100;

                    using (var finalStream = new MemoryStream())
                    {
                        await bitmap.CompressAsync(Bitmap.CompressFormat.Jpeg, (int)percent, finalStream);
                        data = finalStream.GetBuffer();
                    }

                    bitmap.Recycle();

                    GC.Collect();

                    await App.ServerManager.SaveLadingBill(data, _trip.TripId);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    // To do: Show exception message
                    ShowToastMessage.Send("Error save photo.");
                }
            });
        }

        private int GetRotation(string uri)
        {
            var rotate = 0;
            var fileName = this.GetPathToImage(Android.Net.Uri.Parse(uri));
            if (!String.IsNullOrEmpty(fileName))
            {
                ExifInterface exif = new ExifInterface(fileName);
                //int orientation = exif.getAttributeInt(ExifInterface.TAG_ORIENTATION, ExifInterface.ORIENTATION_NORMAL);
                var orientation = exif.GetAttributeInt(ExifInterface.TagOrientation, (int)Android.Media.Orientation.Normal);
                switch (orientation)
                {
                    case (int)Android.Media.Orientation.Rotate270:
                        rotate = 270;
                        break;
                    case (int)Android.Media.Orientation.Rotate180:
                        rotate = 180;
                        break;
                    case (int)Android.Media.Orientation.Rotate90:
                        rotate = 90;
                        break;
                }
            }
            return rotate;
        }

        private string GetPathToImage(Android.Net.Uri uri)
        {
            string doc_id = "";
            using (var c1 = ContentResolver.Query(uri, null, null, null, null))
            {
                c1.MoveToFirst();
                String document_id = c1.GetString(0);
                doc_id = document_id.Substring(document_id.LastIndexOf(":") + 1);
            }

            string path = null;

            // The projection contains the columns we want to return in our query.
            string selection = Android.Provider.MediaStore.Images.Media.InterfaceConsts.Id + " =? ";
            using (var cursor = this.ManagedQuery(Android.Provider.MediaStore.Images.Media.ExternalContentUri, null, selection, new string[] { doc_id }, null))
            {
                if (cursor != null)
                {
                    try
                    {
                        var columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                        cursor.MoveToFirst();
                        path = cursor.GetString(columnIndex);
                    }
                    catch
                    {
                    }
                }
            }
            return path;
        }
    }
    #endregion
}

