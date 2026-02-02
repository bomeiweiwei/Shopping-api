using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Shared.SysConfigs
{
    public class JwtSection
    {
        public string Key { get; }
        public string Issuer { get; }
        public string Audience { get; }

        public JwtSection(IConfiguration config)
        {
            var sec = config.GetRequiredSection("Jwt");
            Key = sec["Key"]
                     ?? throw new InvalidOperationException("Missing Jwt:Key");
            Issuer = sec["Issuer"]
                     ?? throw new InvalidOperationException("Missing Jwt:Issuer");
            Audience = sec["Audience"]
                     ?? throw new InvalidOperationException("Missing Jwt:Audience");
        }
    }
}
