using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using CQRSSpike.Models;
using MediatR;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace CQRSSpike
{
    public class SimpleInjectorInit
    {
        public static void Init()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            var assemblies = GetAssemblies().ToArray();
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof (IRequestHandler<,>), assemblies);
            container.Register(typeof (IAsyncRequestHandler<,>), assemblies);
            container.RegisterCollection(typeof (INotificationHandler<>), assemblies);
            container.RegisterCollection(typeof (IAsyncNotificationHandler<>), assemblies);
            container.RegisterSingleton(Console.Out);
            container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));
            container.RegisterSingleton(new MultiInstanceFactory(container.GetAllInstances));
            container.Register<AccountsContext>(Lifestyle.Scoped);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);


            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof (IMediator).GetTypeInfo().Assembly;
            yield return typeof (BankAccount).GetTypeInfo().Assembly;
        }
    }
}