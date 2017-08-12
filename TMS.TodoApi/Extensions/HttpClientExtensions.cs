using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TMS.TodoApi.Extensions
{
    public static class HttpClientExtensions
    {
        const string JSON_MIME = "application/json";

        public static async Task<T> GetJsonAsync<T>(this HttpClient client, string requestUri)
        {
            var resp = await client.GetAsync(requestUri);
            resp.ThrowHttpResponseExceptions();

            return JsonConvert.DeserializeObject<T>(await resp.Content.ReadAsStringAsync());
        }

        public static async Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient client, string requestUri, T content)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(client.BaseAddress + requestUri),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(content))
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(JSON_MIME);

            var resp = await client.SendAsync(request);
            resp.ThrowHttpResponseExceptions();

            return resp;
        }

        public static async Task<TReturn> PostAndReturnJsonAsync<TObject, TReturn>(this HttpClient client, string requestUri, TObject content)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(client.BaseAddress + requestUri),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(content))
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(JSON_MIME);

            var resp = await client.SendAsync(request);
            resp.ThrowHttpResponseExceptions();

            return JsonConvert.DeserializeObject<TReturn>(await resp.Content.ReadAsStringAsync());
        }

        public static async Task<HttpResponseMessage> PutJsonAsync<T>(this HttpClient client, string requestUri, T content)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(client.BaseAddress + requestUri),
                Method = HttpMethod.Put,
                Content = new StringContent(JsonConvert.SerializeObject(content))
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(JSON_MIME);

            var resp = await client.SendAsync(request);
            resp.ThrowHttpResponseExceptions();

            return resp;
        }
    }
}
