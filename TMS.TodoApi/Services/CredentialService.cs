using System.Linq;
using Windows.Security.Credentials;
using TMS.TodoApi.Interfaces;

namespace TMS.TodoApi.Services
{
    public class CredentialService : ICredentialService
    {
        private PasswordVault _vault;
        private readonly string _resourceName;

        public CredentialService(PasswordVault vault, string resourceName)
        {
            _vault = vault;
            _resourceName = resourceName;
        }

        public PasswordCredential FindFirst()
        {
            return _vault.RetrieveAll()
                .Where(c => c.Resource.Equals(_resourceName))
                .FirstOrDefault();
        }

        public PasswordCredential Save(string userName, string password)
        {
            var credential = new PasswordCredential(_resourceName, userName, password);
            _vault.Add(credential);
            return credential;
        }

        public void Remove(PasswordCredential credential)
        {
            _vault.Remove(credential);
        }
    }
}
