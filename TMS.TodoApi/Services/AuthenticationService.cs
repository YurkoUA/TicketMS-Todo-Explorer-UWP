using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMS.TodoApi.Interfaces;
using TMS.TodoApi.Models;

namespace TMS.TodoApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public bool IsAuthenticated { get; set; }
        public Token AccessToken { get; set; }

        private IHttpService _httpService;

        public AuthenticationService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<bool> TryAuthorizeAsync(string userName, string password)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_httpService.BaseAddress + "token"),
                Method = HttpMethod.Post,
                Content = new StringContent($"grant_type=password&username={userName}&password={password}")
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            request.Headers.UserAgent.ParseAdd("TMS ToDo Explorer UWP");

            var response = await _httpService.Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return false;

            AccessToken = JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());

            if (AccessToken == null)
                return false;
            
            IsAuthenticated = true;
            _httpService.ConfigureToken(AccessToken);

            return true;
        }
    }
}
