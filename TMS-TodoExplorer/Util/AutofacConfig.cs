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

            builder.RegisterType<AuthenticationService>()
                .As<IAuthenticationService>()
                .WithParameter("baseUrl", baseUrl)
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
