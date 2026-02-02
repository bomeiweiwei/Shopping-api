using Microsoft.EntityFrameworkCore;
using MyShop.Domain;
using MyShop.Infrastructure.EF.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Infrastructure.EF
{
    public sealed class EfExecutionStrategyFactory : IExecutionStrategyFactory
    {
        public IExecutionStrategy Create(IMyShopDbContext ctx)
        {
            var db = (ctx as EfMyShopDbContextAdapter)?.Db as MyShopContext
                     ?? throw new InvalidOperationException("Not EF context");

            var efStrategy = db.Database.CreateExecutionStrategy();
            return new StrategyWrapper(efStrategy);
        }

        private sealed class StrategyWrapper : IExecutionStrategy
        {
            private readonly Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy _inner;
            public StrategyWrapper(Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy inner) => _inner = inner;
            public Task ExecuteAsync(Func<Task> operation, CancellationToken ct = default)
                => _inner.ExecuteAsync(operation);
        }
    }
}
