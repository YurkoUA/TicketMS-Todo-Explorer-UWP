using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMS.TodoApi.Exceptions;
using TMS.TodoApi.Services;

namespace TMS.Todo.Tests.Services
{
    [TestClass]
    public class AuthenticationServiceTests
    {
        const string BASE_URL = "http://tms-test2.somee.com/";

        [TestMethod]
        public async Task AuthorizeSuccess()
        {
            var httpService = new HttpService(BASE_URL);
            var authService = new AuthenticationService(httpService);

            var userName = "admin";
            var password = "qwerty";

            await authService.AuthorizeAsync(userName, password);
            
            Assert.IsNotNull(authService.AccessToken);
            Assert.IsNotNull(authService.AccessToken.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task AuthorizeError()
        {
            var httpService = new HttpService(BASE_URL);
            var authService = new AuthenticationService(httpService);

            var userName = "admin";
            var password = "wrong_password";

            await authService.AuthorizeAsync(userName, password);
        }
    }
}
