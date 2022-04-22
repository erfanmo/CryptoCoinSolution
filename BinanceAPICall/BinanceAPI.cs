using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BinanceAPICall
{

    #region Helpers
    public static class BinanceHelpers
    {
        private static readonly Encoding SignatureEncoding = Encoding.UTF8;

        public static string CreateSignature(this string message, string secret)
        {

            byte[] keyBytes = SignatureEncoding.GetBytes(secret);
            byte[] messageBytes = SignatureEncoding.GetBytes(message);
            HMACSHA256 hmacsha256 = new HMACSHA256(keyBytes);

            byte[] bytes = hmacsha256.ComputeHash(messageBytes);

            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

    }

    public class Prices
    {
        public string Symbol { get; set; }
        public double Price { get; set; }
    }

    #endregion

    public static class BinanceAPI
    {

        public static string url = "https://www.binance.com/api/";
        public static string key = "";
        public static string secret = "";
        
        private static string GetTimestamp()
        {
            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds(); 
            return milliseconds.ToString();
        }

        public static string GetJson(string endpoint, string args = null)
        {

            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.BaseAddress = new System.Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-MBX-APIKEY", key);
            HttpResponseMessage messge = client.GetAsync($"{endpoint}?{args}").Result;
            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                description = result;
            }
            else
            {
                string result = "";
                try
                {
                    result = messge.Content.ReadAsStringAsync().Result;
                }
                catch { }
                if (result.Trim() == "")
                    result = messge.ToString();
                throw new Exception(result);
            }
            return description;
        }

        public static string GetSignedJson(string endpoint, string args = null)
        {
            string timestamp = GetTimestamp();
            args += "&timestamp=" + timestamp;
            var signature = args.CreateSignature(secret);
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string headers = client.DefaultRequestHeaders.ToString();
            client.BaseAddress = new System.Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-MBX-APIKEY", key);
            HttpResponseMessage messge = client.GetAsync($"{endpoint}?{args}&signature={signature}").Result;
            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                description = result;
            }
            else
            {
                string result = "";
                try
                {
                    result = messge.Content.ReadAsStringAsync().Result;
                }
                catch { }
                if (result.Trim() == "")
                    result = messge.ToString();
                throw new Exception(result);
            }
            return description;
        }

        public static string PostSignedJson(string endpoint, string args = null)
        {
            string timestamp = GetTimestamp();
            args += "&timestamp=" + timestamp;
            var signature = args.CreateSignature(secret);
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string headers = client.DefaultRequestHeaders.ToString();
            client.BaseAddress = new System.Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-MBX-APIKEY", key);
            HttpResponseMessage messge = client.PostAsync($"{endpoint}?{args}&signature={signature}", null).Result;
            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                description = result;
            }
            else
            {
                string result = "";
                try
                {
                    result = messge.Content.ReadAsStringAsync().Result;
                }
                catch { }
                if (result.Trim() == "")
                    result = messge.ToString();
                throw new Exception(result);
            }
            return description;
        }

        public static string DeleteSignedJson(string endpoint, string args = null)
        {
            string timestamp = GetTimestamp();
            args += "&timestamp=" + timestamp;
            var signature = args.CreateSignature(secret);
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string headers = client.DefaultRequestHeaders.ToString();
            client.BaseAddress = new System.Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-MBX-APIKEY", key);
            HttpResponseMessage messge = client.DeleteAsync($"{endpoint}?{args}&signature={signature}").Result;
            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                description = result;
            }
            else
            {
                string result = "";
                try
                {
                    result = messge.Content.ReadAsStringAsync().Result;
                }
                catch { }
                if (result.Trim() == "")
                    result = messge.ToString();
                throw new Exception(result);
            }
            return description;
        }
    }

}
