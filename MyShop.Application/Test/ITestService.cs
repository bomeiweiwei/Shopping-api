using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application.Test
{
    public interface ITestService
    {
        Task<bool> GetConnectResult();
        Task<bool> TrySetRedis(string key, string value, TimeSpan? expiry = null);
        Task<string> TryGetRedis(string key);
    }
}
