using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using AutofacInterceptors.Infrastructure.Interceptors;
using AutofacInterceptors.Services;

namespace AutofacInterceptors.App_Start.AutofacModules
{
    public class InterceptorsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<LoggerInterceptor>()
                .SingleInstance();

            builder.RegisterType<CacheService>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<CacheInterceptor>()
                .SingleInstance();

            builder.RegisterType<PollyInterceptor>()
                .SingleInstance();
        }
    }
}