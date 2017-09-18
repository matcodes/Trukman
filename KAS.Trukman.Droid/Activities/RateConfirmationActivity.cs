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
using KAS.Trukman.AppContext;
using HockeyApp;
using KAS.Trukman.Data.Enums;

namespace KAS.Trukman.Droid.Activities
{
    [Activity(Name = "com.trukman.ui.rateconfirmationactivity", Label = "RateConfirmationActivity", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class RateConfirmationActivity : Activity
    {
        private Android.Net.Uri pdfUri = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            CrashManager.Register(this, "916e2948dfda4237a1bf0c60188e3f6f");

            this.SetContentView(Resource.Layout.RateConfirmation);

            if (this.Intent.Action == Intent.ActionSend)
            {
                var bundle = this.Intent.Extras;
                pdfUri = (Android.Net.Uri)bundle.Get(Intent.ExtraStream);
            }
            else
                pdfUri = this.Intent.Data;

            TrukmanContext.Initialize();

            var jobButton = this.FindViewById<Button>(Resource.Id.jobButton);
            jobButton.Click += JobButton_Click;

            var signButton = this.FindViewById<Button>(Resource.Id.signButton);
            signButton.Click += SignButton_Click;

            var sendButton = this.FindViewById<Button>(Resource.Id.sendButton);
            sendButton.Click += SendButton_Click;

        }

        private void JobButton_Click(object sender, EventArgs e)
        {
            if (TrukmanContext.User == null)
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Error");
                alert.SetMessage("No user logged in...");
                alert.SetPositiveButton("Ok", (senderAlert, args) => { });
                Dialog dialog = alert.Create();
                dialog.Show();
            }
            else
            {
                if (TrukmanContext.User.Role == UserRole.Owner || TrukmanContext.User.Role == UserRole.Dispatch)
                {
                    var intent = new Intent(this, typeof(OCRPdfActivity));
                    intent.SetFlags(ActivityFlags.GrantReadUriPermission);
                    intent.SetData(pdfUri);
                    this.StartActivity(intent);
                }
                else
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Error");
                    alert.SetMessage("Only owner can create new job...");
                    alert.SetPositiveButton("Ok", (senderAlert, args) => { });
                    Dialog dialog = alert.Create();
                    dialog.Show();
                }
            }
        }

        private void SignButton_Click(object sender, EventArgs e)
        {
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
        }
    }
}