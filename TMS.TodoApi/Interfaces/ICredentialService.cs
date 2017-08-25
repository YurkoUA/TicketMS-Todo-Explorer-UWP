using Windows.Security.Credentials;

namespace TMS.TodoApi.Interfaces
{
    public interface ICredentialService
    {
        PasswordCredential FindFirst();
        PasswordCredential Save(string userName, string password);
        void Remove(PasswordCredential credential);
    }
}
