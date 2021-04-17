using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspnetCoreSession8.Infra.Caching
{
    public class InMemoryCache : ICacheAdapter
    {
        public IMemoryCache MemoryCache { get; }
        public InMemoryCache(IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;
        }

        public void Set<T>(string key, T value) where T : class
        {
            MemoryCache.Set<T>(key, value);
        }

        public Task SetAsync<T>(string key, T value) where T : class
        {
            return Task.Run(() => MemoryCache.Set(key, value));
        }

        public void SetString(string key, string value)
        {
            MemoryCache.Set<string>(key, value);
        }

        public Task SetStringAsync(string key, string value)
        {
            return Task.Run(() => MemoryCache.Set<string>(key, value));
        }

        public T Get<T>(string key) where T : class
        {
            return MemoryCache.Get<T>(key);
        }

        public Task<T> GetAsync<T>(string key) where T : class
        {
            return Task.Run(() => MemoryCache.Get<T>(key));
        }

        public Task<string> GetStringAsync(string key)
        {
            return Task.Run(() => MemoryCache.Get<string>(key));
        }

        public string GetString(string key)
        {
            return MemoryCache.Get<string>(key);
        }

        public Task RemoveAsync(string key)
        {
            return Task.Run(() => MemoryCache.Remove(key));
        }

        public void Remove(string key)
        {
            MemoryCache.Remove(key);
        }

   
    }
}