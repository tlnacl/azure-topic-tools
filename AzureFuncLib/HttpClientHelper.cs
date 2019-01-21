using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzureFuncLib
{
    public static class HttpClientHelper
    {
        // Create a single, static HttpClient https://docs.microsoft.com/en-us/azure/azure-functions/manage-connections
        private static HttpClient httpClient = new HttpClient();

        public static HttpClient GetHttpClient()
        {
            return httpClient;
        }

        public static Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return httpClient.PostAsync(requestUri, content);
        }

        public static Task<HttpResponseMessage> PostAsync(string requestUri, string content)
        {
            var httpContent = new StringContent(content, UnicodeEncoding.UTF8, "application/json");
            return httpClient.PostAsync(requestUri, httpContent);
        }

        public static Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return httpClient.GetAsync(requestUri);
        }

        public static Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return httpClient.SendAsync(request);
        }

        public static Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
        {
            return httpClient.PutAsync(requestUri, content);
        }

        public static Task<HttpResponseMessage> PutAsync(string requestUri, string content)
        {
            var httpContent = new StringContent(content, UnicodeEncoding.UTF8, "application/json");
            return httpClient.PutAsync(requestUri, httpContent);
        }
    }
}
