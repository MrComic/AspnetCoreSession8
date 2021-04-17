using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspnetCoreSession8.Infra.Caching
{
    public interface ICacheAdapter {

        void Set<T>(string key, T value) where T:class;
        Task SetAsync<T>(string key,T value) where T : class;
        void SetString(string key, string value);
        Task SetStringAsync(string key, string value);
        Task<T> GetAsync<T>(string key) where T:class;
        T Get<T>(string key) where T : class;
        Task<string> GetStringAsync(string key);
        string GetString(string key);
        Task RemoveAsync(string key);
        void Remove(string key);

    }
}
