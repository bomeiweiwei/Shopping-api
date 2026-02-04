using System;
using Microsoft.Extensions.Configuration;

namespace MyShop.Shared.SysConfigs
{
    public class ConfigManager
    {
        public static ConnectionStringsSection ConnectionStrings { get; private set; } = default!;
        public static JwtSection Jwt { get; private set; } = default!;
        public static MyShopSection MyShop { get; private set; } = default!;

        public static void Initial(IConfiguration configuration)
        {
            ConnectionStrings = new ConnectionStringsSection(configuration);
            Jwt = new JwtSection(configuration);
            MyShop = new MyShopSection(configuration);
        }
    }
}