using AspnetCoreSession8.Core.DomainModels.Cache;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace AspnetCoreSession8.Infra.Caching
{
    public class CacheFactory
    {
        public static ICacheAdapter CreateCache(IServiceProvider serviceProvider)
        {
            CacheConfiguration c = serviceProvider.GetService<CacheConfiguration>();
            switch (c.cachemode)
            {
                case CacheMode.distributed_memory_cache:
                    {
                        return serviceProvider.GetService<DistributedCache>();
                    }
                case CacheMode.in_memory_cache:
                    {
                        return serviceProvider.GetService<InMemoryCache>();
                    }
                default:
                    throw new Exception("Cache Configuration Is Not Valid");
            }
        }
    }
}
