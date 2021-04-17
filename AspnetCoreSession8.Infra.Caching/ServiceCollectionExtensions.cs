using AspnetCoreSession8.Core.DomainModels.Cache;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspnetCoreSession8.Infra.Caching
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCachingStrategy(this IServiceCollection services, CacheConfiguration c)
        {
            switch (c.cachemode)
            {
                case CacheMode.distributed_memory_cache:
                    {
                        services.AddTransient<DistributedCache>();
                        services.AddDistributedMemoryCache();
                    }break;
                case CacheMode.in_memory_cache:
                    {
                        services.AddTransient<InMemoryCache>();
                        services.AddMemoryCache();
                    }break;
                default:
                    throw new Exception("Cache Configuration Is Not Valid");
            }
        }
    }
}
