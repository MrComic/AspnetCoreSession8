using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace AspnetCoreSession8.Infra.Caching
{
    public class DistributedCache : ICacheAdapter
    {

        public DistributedCache(IDistributedCache cache) {
            Cache = cache;
        }

        public IDistributedCache Cache { get; }

        public T Get<T>(string key) where T : class
        {
            return Cache.Get(key).FromByteArray<T>();
        }

        public async Task<T> GetAsync<T>(string key) where T:class
        {
            var result = await Cache.GetAsync(key);
            return result.FromByteArray<T>();
        }


        public string GetString(string key)
        {
            return Cache.GetString(key);
        }

        public Task<string> GetStringAsync(string key)
        {
            return Cache.GetStringAsync(key);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public Task RemoveAsync(string key)
        {
            return Cache.RemoveAsync(key);
        }

        public void Set<T>(string key, T value) where T : class
        {
            Cache.Set(key, value.ToByteArray());
        }

        public Task SetAsync<T>(string key, T value) where T : class
        {
           return Cache.SetAsync(key, value.ToByteArray());
        }

        public void SetString(string key, string value)
        {
            Cache.SetString(key, value);
        }

        public Task SetStringAsync(string key, string value)
        {
            return Cache.SetStringAsync(key, value);
        }
    }
}