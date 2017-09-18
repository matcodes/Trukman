
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

using Newtonsoft.Json;
using KAS.Trukman.Droid;
using KAS.Trukman.OCR;
using KAS.Trukman.Data.Classes;

namespace KAS.Trukman.Droid.Activities
{
    [Activity(Name = "com.trukman.ui.ocrresultactivity", Label = "OCRResultActivity", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
	public class OCRResultActivity : Activity
	{
        public Trip job;

        private TextView textView;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.OCRResult);
			textView = FindViewById<TextView>(Resource.Id.ResultText);
            var createButton = FindViewById<Button>(Resource.Id.createButton);
            createButton.Click += CreateButton_Click;
			LoadPreferences();
			ShowOrder();
		}

        private void CreateButton_Click(object sender, EventArgs e)
        {
        }

        private void SavePreferences() 
		{
            ISharedPreferences sharedPreferences = GetPreferences(FileCreationMode.Private);
            ISharedPreferencesEditor editor = sharedPreferences.Edit();

            editor.PutString("job", JsonConvert.SerializeObject(job));
            editor.Commit();
        }

		private void LoadPreferences()
		{
            ISharedPreferences sharedPreferences = GetPreferences(FileCreationMode.Private);
            string content = sharedPreferences.GetString("job", null);
            if (!string.IsNullOrEmpty(content))
                job = JsonConvert.DeserializeObject<Trip>(content);
            else
                job = new Trip();
        }

		private void ShowOrder()
		{
            //order.Receiver = Intent.GetStringExtra("Receiver") ?? order.Receiver;
            //order.Sender = Intent.GetStringExtra("Sender") ?? order.Sender;
            //order.ReceiverAddress = Intent.GetStringExtra("ReceiverAddress") ?? order.ReceiverAddress;
            //order.SenderAddress = Intent.GetStringExtra("SenderAddress") ?? order.SenderAddress;
            textView.SetText(job.ToString(), TextView.BufferType.Normal);
        }

		public override void OnBackPressed() 
		{
			SavePreferences();
			base.OnBackPressed();
		}
	}
}

