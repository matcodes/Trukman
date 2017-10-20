
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
        private Trip job;

        private TextView scannedTextView;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.OCRResult);

            this.Title = "Scan Result";

            string scannedText = this.Intent.GetStringExtra("text");

            scannedTextView = FindViewById<TextView>(Resource.Id.ResultText);
            scannedTextView.SetText(scannedText, TextView.BufferType.Normal);

            var createButton = FindViewById<Button>(Resource.Id.createButton);
            createButton.Click += CreateButton_Click;
			LoadPreferences();
            //ShowOrder();

            ShowAlertDialog();
        }

        private void ShowAlertDialog()
        {
            //string[] extraNames = new string[]
            //{
            //    "JobNumber",
            //    "Weight",
            //    "ShipperName",
            //    "LoadFrom",
            //    "PickupTime",
            //    "ReceiverName",
            //    "Destination",
            //    "DropTime",
            //    "Price"
            //};

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

            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            //alert.SetTitle(result);
            alert.SetItems(items, (o, e) =>
            {
                switch (e.Which)
                {
                    case 0:
                        job.JobRef = scannedTextView.Text;
                        break;
                    case 1:
                        int weight = 0;
                        if (Int32.TryParse(scannedTextView.Text, out weight))
                            job.Weight = weight;
                        break;
                    case 2:
                        job.Shipper.Name = scannedTextView.Text;
                        break;
                    case 3:
                        job.FromAddress = scannedTextView.Text;
                        break;
                    case 4:
                        DateTime pickupDate;
                        if (DateTime.TryParse(scannedTextView.Text, out pickupDate))
                            job.PickupDatetime = pickupDate;
                        else
                        {
                            AlertDialog.Builder errorAlert = new AlertDialog.Builder(this);
                            errorAlert.SetTitle("Error");
                            alert.SetMessage("Can not parse your time input. Please try following format: \"April 20 2016 15:00\"");
                            alert.SetPositiveButton("OK", (c, ev) =>
                            {
                            });
                            alert.Show();
                        }
                        break;
                    case 5:
                        job.Receiver.Name = scannedTextView.Text;
                        break;
                    case 6:
                        job.ToAddress = scannedTextView.Text;
                        break;
                    case 7:
                        DateTime dropDate;
                        if (DateTime.TryParse(scannedTextView.Text, out dropDate))
                            job.DeliveryDatetime = dropDate;
                        else
                        {
                            AlertDialog.Builder errorAlert = new AlertDialog.Builder(this);
                            errorAlert.SetTitle("Error");
                            alert.SetMessage("Can not define your time input. Please try following format: \"April 20 2016 15:00\"");
                            alert.SetPositiveButton("OK", (c, ev) =>
                            {
                            });
                            alert.Show();
                            //UIAlertView alert = new UIAlertView("Error", , null, "Ok", null);
                            //alert.Show();
                        }
                        break;
                    case 8:
                        decimal price = 0;
                        if (decimal.TryParse(scannedTextView.Text, out price))
                            job.Price = price;
                        break;
                    default:
                        break;
                }
            });
            alert.SetNegativeButton("Cancel", delegate { this.Finish(); });
            RunOnUiThread(() =>
            {
                alert.Show();
            });

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

		//private void ShowOrder()
		//{
  //          //order.Receiver = Intent.GetStringExtra("Receiver") ?? order.Receiver;
  //          //order.Sender = Intent.GetStringExtra("Sender") ?? order.Sender;
  //          //order.ReceiverAddress = Intent.GetStringExtra("ReceiverAddress") ?? order.ReceiverAddress;
  //          //order.SenderAddress = Intent.GetStringExtra("SenderAddress") ?? order.SenderAddress;
  //          scannedTextView.SetText(job.ToString(), TextView.BufferType.Normal);
  //      }

		public override void OnBackPressed() 
		{
			SavePreferences();
			base.OnBackPressed();
		}
	}
}

