using Autofac;
using TMS.TodoApi.Interfaces;
using TMS.TodoApi.Services;

namespace TMS.TodoExplorer.Util
{
    public class AutofacConfig
    {
        private static IContainer _container;

        public static void ConfigureContainer()
        {
#if DEBUG
            string baseUrl = "http://tms-test.somee.com/";
#else
            string baseUrl = "http://ticket-ms.somee.com/";
#endif

            var builder = new ContainerBuilder();

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
