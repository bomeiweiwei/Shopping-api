using MyShop.Application.Redis;
using MyShop.Domain;
using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application.Test.implement
{
    public class TestService : BaseService, ITestService
    {
        private readonly IRedisService _redis;
        public TestService(IMyShopDbContextFactory factory, IRedisService redis) : base(factory)
        {
            _redis = redis;
        }

        public async Task<bool> GetConnectResult()
        {
            using var ctx = MainDB(ConnectionMode.Slave);
            return await ctx.CanConnectAsync();
        }

        public async Task<bool> TrySetRedis(string key, string value, TimeSpan? expiry = null)
        {
            if (!expiry.HasValue)
                expiry = TimeSpan.FromMinutes(10);

            await _redis.SetStringAsync(key, value, expiry);
            return true;
        }

        public async Task<string> TryGetRedis(string key)
        {
            var val = await _redis.GetStringAsync(key);
            return val;
        }
    }
}
