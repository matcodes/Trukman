using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;

using Newtonsoft.Json;

using Android.Graphics;

namespace Trukman.Droid
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
    public class OCRApi
    {
        private string APIKey;
        private string endpoint = "https://api.ocr.space/Parse/Image";

        public OCRApi(string apikey = "helloworld")
        {
            APIKey = apikey;
        }

        public async Task<OCRResponse> Parse(Bitmap image, string language = "eng")
        {
            HttpClient client = new HttpClient();
            byte[] imageData = BitmapToByteArray(image);
            MultipartFormDataContent form = new MultipartFormDataContent();

            form.Add(new StringContent(APIKey), "apikey");
            form.Add(new StringContent(language), "language");
            form.Add(new ByteArrayContent(imageData, 0, imageData.Length), "image", "image.jpg");

            HttpResponseMessage response = await client.PostAsync(endpoint, form);
            string strContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<OCRResponse>(strContent);
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
    }

    public class OCRResponse
    {
        public Parsedresult[] ParsedResults { get; set; }
        public int OCRExitCode { get; set; }
        public bool IsErroredOnProcessing { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }

    public class Parsedresult
    {
        public object FileParseExitCode { get; set; }
        public string ParsedText { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }
}

