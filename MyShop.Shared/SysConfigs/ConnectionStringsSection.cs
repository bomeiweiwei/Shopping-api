using System;
using Microsoft.Extensions.Configuration;

namespace MyShop.Shared.SysConfigs
{
    public class ConnectionStringsSection
    {
        public string Master { get; }
        public string Slave { get; }

        public string Redis { get; }

        public ConnectionStringsSection(IConfiguration config)
        {
            var sec = config.GetRequiredSection("ConnectionStrings");
            Master = sec["MasterConnection"]
                     ?? throw new InvalidOperationException("Missing ConnectionStrings:MasterConnection");
            Slave = sec["SlaveConnection"]
                     ?? throw new InvalidOperationException("Missing ConnectionStrings:SlaveConnection");
            Redis = sec["RedisConnection"]
                     ?? throw new InvalidOperationException("Missing ConnectionStrings:RedisConnection");
        }
    }
}