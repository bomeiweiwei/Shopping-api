using Microsoft.EntityFrameworkCore;
using MyShop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Infrastructure.EF
{
    internal static class DbContextAccessors
    {
        internal static T AsDbContext<T>(this IMyShopDbContext ctx) where T : DbContext
        {
            if (ctx is EfMyShopDbContextAdapter ef) return (T)ef.Db;
            throw new InvalidOperationException("IMyShopDbContext 不是 EF 的實作。");
        }
    }
}
