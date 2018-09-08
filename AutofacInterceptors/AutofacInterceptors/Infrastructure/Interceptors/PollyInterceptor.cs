using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using Castle.DynamicProxy;
using Polly;

namespace AutofacInterceptors.Infrastructure.Interceptors
{
    public class PollyInterceptor : IInterceptor
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public void Intercept(IInvocation invocation)
        {
            Policy
                .Handle<SqlException>()
                .WaitAndRetry(new[]
                {
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(20),
                    TimeSpan.FromSeconds(50)
                }, (ex, timeSpan, retryCount, context) =>
                {
                    _logger.Error(ex, $"Error - {invocation.TargetType.Name}.{invocation.Method.Name} - try retry (count: {retryCount})");
                })
                .Execute(() => invocation.Proceed());
        }
    }
}