using MyShop.Domain;
using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application.Test.implement
{
    public class TestService : BaseService, ITestService
    {
        public TestService(IMyShopDbContextFactory factory) : base(factory) { }

        public async Task<bool> GetConnectResult()
        {
            using var ctx = MainDB(ConnectionMode.Slave);
            return await ctx.CanConnectAsync();
        }
    }
}
