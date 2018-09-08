using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Extras.DynamicProxy;
using AutofacInterceptors.Infrastructure.Interceptors;
using AutofacInterceptors.Logics;

namespace AutofacInterceptors.App_Start.AutofacModules
{
    public class LogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(typeof(ILogic<>).Assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ILogic<>)))
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LoggerInterceptor))
                .InterceptedBy(typeof(CacheInterceptor));
        }
    }
}