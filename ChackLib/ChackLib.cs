using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChackLib
{
    public class OcrLib
    {
        public static async Task<string> DoOcr(Uri imageUrl)
        {
            using (var client = new HttpClient())
            {
                var postData = "{\"url\": \"" + imageUrl.ToString() + "\"}";
                StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                var url = "https://westus.api.cognitive.microsoft.com/vision/v1.0/ocr";
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Environment.GetEnvironmentVariable("Vision_API_Subscription_Key"));
                var httpResponse = await client.PostAsync(url, content);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return ConvertToText(await httpResponse.Content.ReadAsStringAsync());
                }
            }
            return null;
        }

        public static async Task<string> DoOcr(Stream image)
        {
            using (var client = new HttpClient())
            {
                var content = new StreamContent(image);
                var url = "https://westus.api.cognitive.microsoft.com/vision/v1.0/ocr";
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Environment.GetEnvironmentVariable("Vision_API_Subscription_Key"));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                var httpResponse = await client.PostAsync(url, content);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return ConvertToText(await httpResponse.Content.ReadAsStringAsync());
                }
            }
            return null;
        }

        public static string ConvertToText(string jsonData)
        {
            OcrData ocrData = JsonConvert.DeserializeObject<OcrData>(jsonData);

            OcrResult ocrResult = new OcrResult();

            foreach (Region region in ocrData.Regions)
            {
                foreach (Line line in region.Lines)
                {
                    foreach (Word word in line.Words)
                    {
                        ocrResult.Text += word.Text;
                        if (ocrData.Language != "ja")
                            ocrResult.Text += " ";
                    }
                    ocrResult.Text += "\n";
                }
            }

            return ocrResult.Text;
        }
    }

    public class OcrData
    {
        public string Language { get; set; }
        public string Orientation { get; set; }
        public List<Region> Regions { get; set; }
    }

    public class Region
    {
        public List<Line> Lines { get; set; }
    }

    public class Line
    {
        public List<Word> Words { get; set; }
    }

    public class Word
    {
        public string Text { get; set; }
    }

    public class OcrResult
    {
        public string Text { get; set; }
    }
}
