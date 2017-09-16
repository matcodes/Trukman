
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

namespace Trukman.Droid
{
	[Activity(Label = "OCRActivity")]			
	public class OCRActivity : Activity
	{
		private Order order;
		private TextView textView;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.OCRResult);
			textView = FindViewById<TextView>(Resource.Id.ResultText);
            //var createButton = FindViewById<Button>(Resource.Id.createButton);
            //createButton.Click += CreateButton_Click;
			LoadPreferences();
			ShowOrder();
		}

        //private void CreateButton_Click(object sender, EventArgs e)
        //{
        //}

        private void SavePreferences() 
		{
			ISharedPreferences sharedPreferences = GetPreferences(FileCreationMode.Private);
			ISharedPreferencesEditor editor = sharedPreferences.Edit();

			editor.PutString("order", JsonConvert.SerializeObject(order));
			editor.Commit();
		}

		private void LoadPreferences()
		{
			ISharedPreferences sharedPreferences = GetPreferences(FileCreationMode.Private);
			string strOrder = sharedPreferences.GetString("order", null);
			if (strOrder != null)
			{
				order = JsonConvert.DeserializeObject<Order>(strOrder);
			}
			else
			{
				order = new Order();
			}
		}

		private void ShowOrder()
		{
			order.Receiver = Intent.GetStringExtra("Receiver") ?? order.Receiver;
			order.Sender = Intent.GetStringExtra("Sender") ?? order.Sender;
			order.ReceiverAddress = Intent.GetStringExtra("ReceiverAddress") ?? order.ReceiverAddress;
			order.SenderAddress = Intent.GetStringExtra("SenderAddress") ?? order.SenderAddress;
			textView.SetText(order.GetOrder(), TextView.BufferType.Normal);
		}

		public override void OnBackPressed() 
		{
			SavePreferences();
			base.OnBackPressed();
		}
	}
}

