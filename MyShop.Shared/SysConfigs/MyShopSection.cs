using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Shared.SysConfigs
{
    public sealed class MyShopSection
    {
        public string LogRoot { get; }
        public ApiLoggingSection ApiLogging { get; }

        public MyShopSection(IConfiguration config)
        {
            var sec = config.GetRequiredSection("MyShop");

            LogRoot = sec["LogRoot"]
                ?? throw new InvalidOperationException("Missing MyShop:LogRoot");

            ApiLogging = new ApiLoggingSection(sec);
        }
    }
}
