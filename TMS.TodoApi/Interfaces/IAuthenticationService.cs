using System.Threading.Tasks;
using Windows.Security.Credentials;
using TMS.TodoApi.Models;

namespace TMS.TodoApi.Interfaces
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; set; }
        Token AccessToken { get; set; }

        PasswordCredential Credential { get; set; }

        Task AuthorizeAsync(string userName, string password, bool isRemember = false);
        void Logout();
    }
}
