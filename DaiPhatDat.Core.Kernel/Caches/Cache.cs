using System;
using System.Runtime.Caching;

namespace DaiPhatDat.Core.Kernel.Caches
{
    public class Cache : ICache
    {
        public void Set<T>(string cacheKey, Func<T> getItemCallback) where T : class
        {
            try
            {
                T item = getItemCallback();
                cacheKey = cacheKey.ToUpper();
                if (item != null)
                {
                    if (MemoryCache.Default.Contains(cacheKey))
                    {
                        MemoryCache.Default.Set(cacheKey, item, DateTimeOffset.MaxValue);
                    }
                    else
                    {
                        MemoryCache.Default.Add(cacheKey, item, DateTimeOffset.MaxValue);
                    }
                }
            }
            catch { }
        }
        public void SetByTime<T>(string cacheKey, Func<T> getItemCallback, DateTimeOffset absoluteExpiration) where T : class
        {
            try
            {
                T item = getItemCallback();
                cacheKey = cacheKey.ToUpper();
                if (item != null)
                {
                    if (MemoryCache.Default.Contains(cacheKey))
                    {
                        MemoryCache.Default.Set(cacheKey, item, absoluteExpiration);
                    }
                    else
                    {
                        MemoryCache.Default.Add(cacheKey, item, absoluteExpiration);
                    }
                }
            }
            catch { }
        }
        public T Get<T>(string cacheKey) where T : class
        {
            cacheKey = cacheKey.ToUpper();
            T item = MemoryCache.Default.Get(cacheKey) as T;
            return item;
        }
        public bool Remove(string cacheKey)
        {
            try
            {
                MemoryCache.Default.Remove(cacheKey);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveAll()
        {
            try
            {
                MemoryCache.Default.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
