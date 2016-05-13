using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;

using Newtonsoft.Json;


namespace KAS.Trukman.OCR
{
	/*
     * Usage example:
     * OCRApi ocr = new OCRApi();
            
       Task parseTask = ocr.Parse(selectedFragment)
           .ContinueWith((task) =>
            {
               OCRResponse response = task.Result;
               // And so on...
            });
     */
	public class Order
	{
		public string Sender { get; set; }
		public string Receiver { get; set; }
		public string ReceiverAddress { get; set; }
		public string SenderAddress { get; set; }

		public String GetOrder() 
		{
			return string.Format("Sender: {0}\nReceiver: {1}\nLoad address: {2}\nReceiver address: {3}", 
				Sender, Receiver, SenderAddress, ReceiverAddress);
		}

		#region Constructors
		public Order() { }

		public Order(string sender, string receiver, string senderAddress, string receiverAddress)
		{
			Sender = sender;
			Receiver = receiver;
			ReceiverAddress = receiverAddress;
			SenderAddress = senderAddress;
		}

		public Order(string[] args) 
		{
			try {
				Sender = args[0];
				Receiver = args[1];
				ReceiverAddress = args[2];
				SenderAddress = args[3];
			} catch(IndexOutOfRangeException) {}
		}
		#endregion
	}

	public class OCRResponse
	{
		[JsonConstructor]
		public OCRResponse() { }

		public Parsedresult[] ParsedResults { get; set; }
		public int OCRExitCode { get; set; }
		public bool IsErroredOnProcessing { get; set; }
		public string ErrorMessage { get; set; }
		public string ErrorDetails { get; set; }
	}

	public class Parsedresult
	{
		[JsonConstructor]
		public Parsedresult() { }

		public int FileParseExitCode { get; set; }
		public string ParsedText { get; set; }
		public string ErrorMessage { get; set; }
		public string ErrorDetails { get; set; }
	}

	public class OCRApi
	{
		private string APIKey;
		private string endpoint = "https://api.ocr.space/Parse/Image";

		public OCRApi(string apikey = "helloworld")
		{
			APIKey = apikey;
		}

		public async Task<OCRResponse> Parse(byte[] imageData, string language = "eng")
		{
			HttpClient client = new HttpClient();
			MultipartFormDataContent form = new MultipartFormDataContent();

			form.Add(new StringContent(APIKey), "apikey");
			form.Add(new StringContent(language), "language");
			form.Add(new ByteArrayContent(imageData, 0, imageData.Length), "image", "image.jpg");

			HttpResponseMessage response = await client.PostAsync(endpoint, form);
			string strContent = await response.Content.ReadAsStringAsync();

			Console.WriteLine ("OCR request result: {0}", strContent);
			return JsonConvert.DeserializeObject<OCRResponse>(strContent);
		}
	}
}
