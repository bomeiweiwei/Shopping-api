using StackExchange.Redis;

namespace MyShop.Application.Redis.implement
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _db;

        public RedisService(IConnectionMultiplexer mux)
        {
            _db = mux.GetDatabase();
        }

        public Task SetStringAsync(string key, string value, TimeSpan? expiry = null)
            => _db.StringSetAsync(key, value, expiry);

        public async Task<string?> GetStringAsync(string key)
        {
            var val = await _db.StringGetAsync(key);
            return val.HasValue ? val.ToString() : null;
        }

        public Task<bool> RemoveAsync(string key)
            => _db.KeyDeleteAsync(key);
    }
}
