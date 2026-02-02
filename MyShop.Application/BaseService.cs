using MyShop.Domain;
using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application
{
    public class BaseService
    {
        private readonly IMyShopDbContextFactory _factory;
        protected BaseService(IMyShopDbContextFactory factory) => _factory = factory;

        protected IMyShopDbContext MainDB(ConnectionMode mode = ConnectionMode.Master) => _factory.Create(mode);
    }
}
