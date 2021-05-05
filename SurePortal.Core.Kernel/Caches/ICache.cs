using System;

namespace DaiPhatDat.Core.Kernel.Caches
{
    public interface ICache
    {
        /// <summary>
        /// Set caching by max time
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="getItemCallback"></param>
        void Set<T>(string cacheKey, Func<T> getItemCallback) where T : class;
        /// <summary>
        /// Set caching by time
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="getItemCallback"></param>
        /// <param name="absoluteExpiration"></param>
        void SetByTime<T>(string cacheKey, Func<T> getItemCallback, DateTimeOffset absoluteExpiration) where T : class;
        /// <summary>
        /// Get caching
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        T Get<T>(string cacheKey) where T : class;
        /// <summary>
        /// Remove caching
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        bool Remove(string cacheKey);
        /// <summary>
        /// Remove all caching
        /// </summary>
        /// <returns></returns>
        bool RemoveAll();
    }
}
