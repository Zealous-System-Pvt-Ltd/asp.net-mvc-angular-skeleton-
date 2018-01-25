using System.Runtime.Caching;

namespace Demo.Business.Contracts
{
    public interface ICacheManager
    {
        /// <summary>
        /// Gets or sets the cache.
        /// </summary>
        MemoryCache Cache { get;set; }

        /// <summary>
        /// The get from cache.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <typeparam name="T"> The generic object  </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T GetFromCache<T>(string key) where T : class;

        /// <summary>
        /// The add to cache.
        /// </summary>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <param name="objectTobeAdded">
        ///     The object tobe added.
        /// </param>
        /// <param name="cacheItemPolicy"></param>
        void AddToCache(string key, object objectTobeAdded, CacheItemPolicy cacheItemPolicy);

        /// <summary>
        /// The remove from cache.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        void RemoveFromCache(string key);

        /// <summary>
        /// The does cache contains.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool DoesCacheContains(string key);
    }
}