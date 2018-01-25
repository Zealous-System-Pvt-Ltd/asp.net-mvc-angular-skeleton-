using Demo.Business.Contracts;
using System.Runtime.Caching;

namespace Demo.Business.Manager
{
    /// <summary>
    /// maintain cache at manager lavel so web response make faster
    /// </summary>
    public class CacheManager : ICacheManager
    {
        public CacheManager()
        {
            Cache = MemoryCache.Default;
        }

        /// <summary>
        /// Gets the cache.
        /// </summary>
        public MemoryCache Cache { get; set; }

        public T GetFromCache<T>(string key) where T : class
        {
            var o = Cache.Get(key) as T;
            return o;
        }

        public void AddToCache(string key, object objectTobeAdded, CacheItemPolicy cacheItemPolicy)
        {
            lock (Cache)
            {
                Cache.Add(key, objectTobeAdded, cacheItemPolicy);
            }
        }

        public void RemoveFromCache(string key)
        {
            lock (Cache)
            {
                Cache.Remove(key);
            }
        }

        public bool DoesCacheContains(string key)
        {
            return Cache.Contains(key);
        }
    }
}