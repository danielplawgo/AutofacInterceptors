using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Extras.DynamicProxy;
using AutofacInterceptors.Infrastructure.Interceptors;
using AutofacInterceptors.Models;
using AutofacInterceptors.Repositories;

namespace AutofacInterceptors.App_Start.AutofacModules
{
    public class DataContextModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataContext>()
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(Repository<>).Assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<>)))
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LoggerInterceptor))
                .InterceptedBy(typeof(PollyInterceptor));
        }
    }
}