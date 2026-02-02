using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Domain
{
    public interface IMyShopDbContext : IDisposable
    {
        // 連線測試
        Task<bool> CanConnectAsync(CancellationToken ct = default);
        // 交易
        Task<int> SaveChangesAsync(CancellationToken ct = default);

        Task<ITransaction> BeginTransactionAsync(CancellationToken ct = default);
    }
}
