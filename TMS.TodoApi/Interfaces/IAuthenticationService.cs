using System.Threading.Tasks;
using TMS.TodoApi.Models;

namespace TMS.TodoApi.Interfaces
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; set; }
        Token AccessToken { get; set; }

        Task AuthorizeAsync(string userName, string password);
    }
}
