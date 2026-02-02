using Microsoft.EntityFrameworkCore;
using MyShop.Domain;
using MyShop.Infrastructure.EF.Data;
using MyShop.Infrastructure.EF.Extensions;
using MyShop.Shared.Enums;
using MyShop.Shared.SysConfigs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Infrastructure.EF
{
    public sealed class MyShopDbContextFactory : IMyShopDbContextFactory
    {
        public IMyShopDbContext Create(ConnectionMode mode = ConnectionMode.Master)
        {
            var options = new DbContextOptionsBuilder<MyShopContext>();

            var cs = mode == ConnectionMode.Master
                ? ConfigManager.ConnectionStrings.Master
                : ConfigManager.ConnectionStrings.Slave;

            options.OptionsBuilderSetting(cs);

            var ctx = new MyShopContext(options.Options);
            return new EfMyShopDbContextAdapter(ctx);
        }
    }
}
