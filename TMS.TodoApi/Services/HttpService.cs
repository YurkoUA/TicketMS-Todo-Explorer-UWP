using System;
using System.Net.Http;
using System.Net.Http.Headers;
using TMS.TodoApi.Interfaces;
using TMS.TodoApi.Models;

namespace TMS.TodoApi.Services
{
    public class HttpService : IHttpService
    {
        public HttpClient Client { get; set; }
        public string BaseAddress { get; set; }

        public HttpService(string baseUrl)
        {
            BaseAddress = baseUrl + "api/";

            Client = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };
        }

        public void ConfigureToken(Token accessToken)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(accessToken.TokenType, accessToken.ToString());
        }
    }
}
