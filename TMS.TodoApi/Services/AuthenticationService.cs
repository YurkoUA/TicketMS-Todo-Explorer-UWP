using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Newtonsoft.Json;
using TMS.TodoApi.Exceptions;
using TMS.TodoApi.Interfaces;
using TMS.TodoApi.Models;

namespace TMS.TodoApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public bool IsAuthenticated { get; set; }
        public Token AccessToken { get; set; }

        public PasswordCredential Credential { get; set; }

        private IHttpService _httpService;
        private ICredentialService _credentialService;

        public AuthenticationService(IHttpService httpService, ICredentialService credentialService)
        {
            _httpService = httpService;
            _credentialService = credentialService;
        }

        public async Task AuthorizeAsync(string userName, string password, bool isRemember = false)
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
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new BadRequestException(response);
                }
                throw new HttpResponseException(response);
            }

            AccessToken = JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());

            if (AccessToken == null)
                throw new HttpResponseException(response);
            
            IsAuthenticated = true;
            _httpService.ConfigureAuthorization(AccessToken);

            if (isRemember)
            {
                Credential = _credentialService.Save(userName, password);
            }
        }

        public void Logout()
        {
            IsAuthenticated = false;
            AccessToken = null;
            _httpService.ResetAuthorization();

            if (Credential != null)
            {
                _credentialService.Remove(Credential);
                Credential = null;
            }
        }
    }
}
