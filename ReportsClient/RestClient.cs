using Models.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;




namespace ReportsClient
{
    public class RestClient
    {
        private static HttpClient client;
        public string Response { get; set; }

        public RestClient(string url)
        {
            client = new HttpClientFactory().CreateClient(url);

        }

        public async Task Get(string path)
        {
            try
            {
                using (var response = await client.GetAsync(path))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        Response = response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (TaskCanceledException ex)
            {

            }  
        }

        public class HttpClientFactory : IHttpClientFactory
        {
            
            public HttpClient CreateClient(string url)
            {
         
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                SetupClientDefaults(client);
                return client;
            }

            protected virtual void SetupClientDefaults(HttpClient client)
            {
                client.Timeout = TimeSpan.FromMinutes(30);  
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

    }
        public interface IHttpClientFactory
        {
            HttpClient CreateClient(string url);
        }
    }
