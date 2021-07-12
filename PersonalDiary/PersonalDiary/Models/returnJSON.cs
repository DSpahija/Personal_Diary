using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PersonalDiary.Models
{
    class returnJSON
    {
        public static string getJSON(string url)
        {
            HttpClient client = new HttpClient();

            string response = client.GetStringAsync(url).ToString();

            return response;
        }

        public static string GetURL()
        {
            return "http://192.168.0.17:8080/";
        }
    }
}
