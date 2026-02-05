using MyShop.Domain;
using MyShop.Domain.Account;
using MyShop.Infrastructure.EF;
using MyShop.Infrastructure.EF.Data;
using MyShop.Models.Dto.Account;
using MyShop.Models.Resp.Account;
using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Infrastructure.Repositories.Account
{
    public class AdminAccountCreateRepository : IAdminAccountCreateRepository
    {
        /// <summary>
        /// 建立後台管理員帳號(步驟二)
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="dto"></param>
        /// <param name="accountId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<AdminCreateResp> StepCreateAdminAccountAsync(IMyShopDbContext ctx, AdminCreateDto dto, long accountId, CancellationToken ct = default)
        {
            var result = new AdminCreateResp();
            var db = ctx.AsDbContext<MyShopContext>();
            var adminaccount = new EF.Models.AdminAccount
            {
                AccountId = accountId,
                DisplayName = dto.Username,
                Status = (int)Status.Active,
                CreatedAt = dto.CreatedAt,
                CreatedBy = dto.CreatedBy
            };
            await db.AdminAccounts.AddAsync(adminaccount, ct);
            await ctx.SaveChangesAsync(ct);

            foreach (var roleId in dto.RoleIds)
            {
                var adminAccountAdminRole = new EF.Models.AdminAccountAdminRole
                {
                    AdminAccountId = adminaccount.AdminAccountId,
                    AdminRoleId = roleId
                };
                await db.AdminAccountAdminRoles.AddAsync(adminAccountAdminRole, ct);
            }
            await ctx.SaveChangesAsync(ct);

            result.AdminAccountId = adminaccount.AdminAccountId;
            result.Username = dto.Username;
            result.CreatedAt = dto.CreatedAt;

            return result;
        }
    }
}
