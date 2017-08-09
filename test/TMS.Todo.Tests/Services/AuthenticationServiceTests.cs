using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMS.TodoApi.Services;

namespace TMS.Todo.Tests.Services
{
    [TestClass]
    public class AuthenticationServiceTests
    {
        const string BASE_URL = "http://tms-test.somee.com/";

        [TestMethod]
        public async Task AuthorizeSuccess()
        {
            var authService = new AuthenticationService(BASE_URL);

            var userName = "admin";
            var password = "qwerty";

            var authResult = await authService.TryAuthorizeAsync(userName, password);

            Assert.IsTrue(authResult);
            Assert.IsNotNull(authService.AccessToken);
            Assert.IsNotNull(authService.AccessToken.ToString());
        }

        [TestMethod]
        public async Task AuthorizeError()
        {
            var authService = new AuthenticationService(BASE_URL);

            var userName = "admin";
            var password = "wrong_password";

            var authResult = await authService.TryAuthorizeAsync(userName, password);

            Assert.IsFalse(authResult);
            Assert.IsNull(authService.AccessToken);
        }
    }
}
