using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Domain
{
    public interface IExecutionStrategyFactory
    {
        IExecutionStrategy Create(IMyShopDbContext ctx);
    }
}
