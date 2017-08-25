using Windows.Security.Credentials;
using Autofac;
using TMS.TodoApi.Interfaces;
using TMS.TodoApi.Services;

namespace TMS.TodoExplorer.Util
{
    public static class AutofacConfig
    {
        private static IContainer _container;

        public static void ConfigureContainer()
        {
#if DEBUG
            string baseUrl = "http://tms-test2.somee.com/";
#else
            string baseUrl = "http://ticket-ms.somee.com/";
#endif

            var builder = new ContainerBuilder();

            #region Bindings

            builder.RegisterType<HttpService>()
                .As<IHttpService>()
                .WithParameter("baseUrl", baseUrl)
                .SingleInstance();

            builder.RegisterType<NavigationService>()
                .As<INavigationService>()
                .SingleInstance();

            builder.RegisterType<AuthenticationService>()
                .As<IAuthenticationService>()
                .SingleInstance();

            builder.RegisterType<TodoService>()
                .As<ITodoService>()
                .SingleInstance();

            builder.RegisterType<CredentialService>()
                .As<ICredentialService>()
                .WithParameter("resourceName", "TMS Todo Explorer App")
                .WithParameter("vault", new PasswordVault())
                .SingleInstance();

            #endregion

            _container = builder.Build();
        }

        public static T Resolve<T>()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                return scope.Resolve<T>();
            }
        }
    }
}
