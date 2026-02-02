using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application.Redis
{
    public interface IRedisService
    {
        Task SetStringAsync(string key, string value, TimeSpan? expiry = null);
        Task<string?> GetStringAsync(string key);
        Task<bool> RemoveAsync(string key);
    }
}
