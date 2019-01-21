using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutofacInterceptors.Logics;
using AutofacInterceptors.Models;
using AutofacInterceptors.Services;
using Castle.DynamicProxy;

namespace AutofacInterceptors.Infrastructure.Interceptors
{
    public class CacheInterceptor : IInterceptor
    {
        private ICacheService _cacheService;

        public CacheInterceptor(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public void Intercept(IInvocation invocation)
        {
            var isLogic = invocation.TargetType.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ILogic<>));

            if (isLogic == false)
            {
                invocation.Proceed();
                return;
            }

            var isGetById = invocation.Method.Name == nameof(ILogic<BaseModel>.GetById);

            if (isGetById)
            {
                ProceedGetById(invocation);
                return;
            }

            ProceedOtherMethods(invocation);
        }

        private void ProceedOtherMethods(IInvocation invocation)
        {
            var methodsToClear = new[] {nameof(ILogic<BaseModel>.Update), nameof(ILogic<BaseModel>.Delete)};
            var clearCache = methodsToClear.Contains(invocation.Method.Name);

            string key = "";

            if (clearCache)
            {
                key = KeyFromObject(invocation);
            }

            invocation.Proceed();

            if (clearCache)
            {
                _cacheService.Remove(key);
            }
        }

        private void ProceedGetById(IInvocation invocation)
        {
            bool invoked = false;
            var result = _cacheService.GetOrAdd(KeyFromParameter(invocation, invocation.Arguments[0] as int?), () =>
            {
                invocation.Proceed();
                invoked = true;
                return invocation.ReturnValue;
            });

            if (invoked == false)
            {
                invocation.ReturnValue = result;
            }

            return;
        }

        private string KeyFromParameter(IInvocation invocation, int? id)
        {
            return $"{invocation.TargetType.FullName}-{id}";
        }

        private string KeyFromObject(IInvocation invocation)
        {
            var model = invocation.Arguments[0] as BaseModel;

            if (model == null)
            {
                throw new ArgumentException("Wrong parameter type");
            }

            return $"{invocation.TargetType.FullName}-{model.Id}";
        }
    }
}