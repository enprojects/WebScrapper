using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface  ICacheService
    {
        Task<T> GetOrCreateItemAsync<T>(string key, Func<Task<T>> func = null);
        T GetOrCreateItemAsync<T>(string key, T values = default(T));
    }
}
