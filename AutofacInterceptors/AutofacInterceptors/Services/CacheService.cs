using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CacheManager.Core;
using NLog.Extensions.Logging;

namespace AutofacInterceptors.Services
{
    public class CacheService : ICacheService
    {
        private ICacheManager<object> _cache;

        private const string _defaultCacheName = "defaultCache";

        public CacheService()
        {
            var builder = ConfigurationBuilder.LoadConfiguration(_defaultCacheName).Builder;
            builder.WithMicrosoftLogging(s => s.AddNLog());

            _cache = CacheFactory.FromConfiguration<object>(_defaultCacheName, builder.Build());
        }

        public T GetOrAdd<T>(string key, Func<T> getFunc)
        {
            return (T)_cache.GetOrAdd(key, k => getFunc());
        }

        public T GetOrAdd<T>(string key, string region, Func<T> getFunc)
        {
            return (T)_cache.GetOrAdd(key, region, (k, r) => getFunc());
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Remove(string key, string region)
        {
            _cache.Remove(key, region);
        }

        public void Clear()
        {
            _cache.Clear();
        }

        public void ClearRegion(string region)
        {
            _cache.ClearRegion(region);
        }
    }
}