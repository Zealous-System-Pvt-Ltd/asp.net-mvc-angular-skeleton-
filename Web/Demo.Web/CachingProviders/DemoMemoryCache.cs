using Demo.Business.Contracts;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using WebApi.OutputCache.Core.Cache;

namespace Demo.Web.CachingProviders
{
    public class DemoMemoryCache : IApiOutputCache
    {
        private readonly ICacheManager _cacheManager;

        public DemoMemoryCache()
        {
            _cacheManager = ServiceLocator.Current.GetInstance<ICacheManager>();
        }

        public void RemoveStartsWith(string key)
        {
            _cacheManager.RemoveFromCache(key);
        }

        public T Get<T>(string key) where T : class
        {
            return _cacheManager.GetFromCache<T>(key);
        }

        public object Get(string key)
        {
            return _cacheManager.Cache.Get(key);
        }

        public void Remove(string key)
        {
            _cacheManager.RemoveFromCache(key);
        }

        public bool Contains(string key)
        {
            return _cacheManager.DoesCacheContains(key);
        }

        public void Add(string key, object o, DateTimeOffset expiration, string dependsOnKey = null)
        {
            var cachePolicy = new CacheItemPolicy { AbsoluteExpiration = expiration };

            if (!string.IsNullOrWhiteSpace(dependsOnKey))
            {
                cachePolicy.ChangeMonitors.Add(_cacheManager.Cache.CreateCacheEntryChangeMonitor(new[] { dependsOnKey }));
            }
            _cacheManager.AddToCache(key, o, cachePolicy);
        }

        public IEnumerable<string> AllKeys
        {
            get
            {
                return _cacheManager.Cache.Select(x => x.Key);
            }
        }
    }
}