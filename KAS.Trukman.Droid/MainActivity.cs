using System;

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

namespace KAS.Trukman.Droid
{
    #region MainActivity
    [Activity (Label = "TRUKMAN", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
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
        }

        protected override void OnPause()
        {
            ShowToastMessage.Unsubscribe(this);
            ShowGPSSettingsMessage.Unsubscribe(this);

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
    }
    #endregion
}

