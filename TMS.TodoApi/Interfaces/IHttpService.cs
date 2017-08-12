using System.Net.Http;
using TMS.TodoApi.Models;

namespace TMS.TodoApi.Interfaces
{
    public interface IHttpService
    {
        HttpClient Client { get; set; }
        string BaseAddress { get; set; }

        void ConfigureToken(Token accessToken);
    }
}
