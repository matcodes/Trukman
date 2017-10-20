using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace KAS.Trukman.OCR
{
    #region OCRResponse
    public class OCRResponse
    {
        [JsonConstructor]
        public OCRResponse() { }

        public List<Parsedresult> ParsedResults { get; set; }
        public int OCRExitCode { get; set; }
        public bool IsErroredOnProcessing { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }
    #endregion

    #region Parsedresult
    public class Parsedresult
    {
        [JsonConstructor]
        public Parsedresult() { }

        public int FileParseExitCode { get; set; }
        public string ParsedText { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }
    #endregion

    #region OCRApi
    public class OCRApi
    {
        private string APIKey = "c7b87bd78d88957";
        private string endpoint = "https://api.ocr.space/Parse/Image";

        public OCRApi()
        {
        }

        public async Task<OCRResponse> Parse(byte[] imageData, string language = "eng")
        {
            HttpClient client = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();

            form.Add(new StringContent(APIKey), "apikey");
            form.Add(new StringContent(language), "language");
            form.Add(new ByteArrayContent(imageData, 0, imageData.Length), "file", "image.jpg");

            HttpResponseMessage response = await client.PostAsync(endpoint, form);
            string strContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine("OCR request result: {0}", strContent);
            return JsonConvert.DeserializeObject<OCRResponse>(strContent);
        }
    }
    #endregion
}
