using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Services
{
    public class CacheService : ICacheService
    {
        private IMemoryCache _cache;
        public CacheService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public async Task<T> GetOrCreateItemAsync<T>(string key, Func<Task<T>> func = null)
        {
          object  value;

            if (!_cache.TryGetValue(key, out  value))
            {
                
                if (func != null)
                {
                    value = await func();
                    if (value != null)
                    {
                        _cache.Set(key, value);
                    }
                }
            }

            return (T)value;
        }

        public  T GetOrCreateItemAsync<T>(string key, T values =default(T))
        {
            T value;

            if (!_cache.TryGetValue(key, out value))
            {
                if (values != null)
                {
                    _cache.Set(key, values);
                }
            }

            return value;
        }
    }
}
