using Microsoft.EntityFrameworkCore;
using MyShop.Domain;
using MyShop.Domain.AdminPermission;
using MyShop.Infrastructure.EF;
using MyShop.Infrastructure.EF.Data;
using MyShop.Models.Dto.AccountRole;
using MyShop.Models.Req.AdminPermission;
using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Infrastructure.Repositories.AdminPermission
{
    public sealed class AdminPermissionReadRepository : IAdminPermissionReadRepository
    {
        private readonly IMyShopDbContextFactory _factory;
        public AdminPermissionReadRepository(IMyShopDbContextFactory factory) => _factory = factory;
        public async Task<List<long>> GetAdminPermissionsData(GetAdminPermissionsDataReq req, CancellationToken ct = default)
        {
            var result = new List<long>();
            using var ctx = _factory.Create(ConnectionMode.Slave);
            var db = ctx.AsDbContext<MyShopContext>();

            result =
                await (from m in db.Accounts
                       join a1 in db.AdminAccounts on m.AccountId equals a1.AccountId
                       join a2 in db.AdminAccountAdminRoles on a1.AdminAccountId equals a2.AdminAccountId
                       join a3 in db.AdminRoles on a2.AdminRoleId equals a3.AdminRoleId
                       join a4 in db.AdminRolePermissions on a3.AdminRoleId equals a4.AdminRoleId
                       join p in db.Permissions on a4.PermissionId equals p.PermissionId
                       where m.AccountId == req.AccountId
                       select p.PermissionId).Distinct().ToListAsync(ct);

            return result;
        }
    }
}
