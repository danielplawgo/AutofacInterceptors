using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.DynamicProxy;

namespace AutofacInterceptors.Infrastructure.Interceptors
{
    public class LoggerInterceptor : IInterceptor
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public void Intercept(IInvocation invocation)
        {
            _logger.Info($"Before call: {invocation.TargetType.Name}.{invocation.Method.Name}");

            invocation.Proceed();

            _logger.Info($"After call: {invocation.TargetType.Name}.{invocation.Method.Name}");
        }
    }
}