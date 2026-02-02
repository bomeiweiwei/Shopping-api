using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Domain
{
    public interface IMyShopDbContextFactory
    {
        IMyShopDbContext Create(ConnectionMode mode = ConnectionMode.Master);
    }
}
