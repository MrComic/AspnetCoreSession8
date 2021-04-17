using System;
using System.Collections.Generic;
using System.Text;

namespace AspnetCoreSession8.Core.DomainModels.Cache
{
    public enum CacheMode {
        //sqlserver= 1,
        //redis= 2,
        distributed_memory_cache = 3,
        in_memory_cache= 4
    }

    public class CacheConfiguration
    {
        public CacheMode cachemode { get; set; }

        public string connectionstring { get; set; }

        public int expiration { get; set; }
    }
}
