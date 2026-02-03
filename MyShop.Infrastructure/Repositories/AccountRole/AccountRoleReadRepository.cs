using Microsoft.EntityFrameworkCore;
using MyShop.Domain;
using MyShop.Domain.AccountRole;
using MyShop.Infrastructure.EF;
using MyShop.Infrastructure.EF.Data;
using MyShop.Models.Dto.AccountRole;
using MyShop.Models.Req.AccountRole;
using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Infrastructure.Repositories.AccountRole
{
    public sealed class AccountRoleReadRepository : IAccountRoleReadRepository
    {
        private readonly IMyShopDbContextFactory _factory;
        public AccountRoleReadRepository(IMyShopDbContextFactory factory) => _factory = factory;

        public async Task<AccountRoleDto?> GetAccountRoleData(GetAccountRoleReq req, CancellationToken ct = default)
        {
            var result = new AccountRoleDto();
            using var ctx = _factory.Create(ConnectionMode.Slave);
            var db = ctx.AsDbContext<MyShopContext>();

            var accountRole = await db.AccountRoles.FirstOrDefaultAsync(m => m.AccountId == req.AccountId && m.RoleId == req.RoleId, ct);
            if (accountRole == null)
                return null;

            result.AccountId = accountRole.AccountId;
            result.RoleId = accountRole.RoleId;

            return result;
        }
    }
}
