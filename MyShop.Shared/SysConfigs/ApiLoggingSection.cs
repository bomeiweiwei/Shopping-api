using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Shared.SysConfigs
{
    public sealed class ApiLoggingSection
    {
        public string[] SensitivePathKeywords { get; }

        public ApiLoggingSection(IConfiguration myShopSection)
        {
            var sec = myShopSection.GetRequiredSection("ApiLogging");

            SensitivePathKeywords = sec.GetSection("SensitivePathKeywords").Get<string[]>()
                ?? throw new InvalidOperationException("Missing MyShop:ApiLogging:SensitivePathKeywords");
        }
    }
}
