using Microsoft.EntityFrameworkCore;
using MyShop.Domain;
using MyShop.Domain.Account;
using MyShop.Infrastructure.EF;
using MyShop.Infrastructure.EF.Data;
using MyShop.Models.Dto.Account;
using MyShop.Models.Exceptions;
using MyShop.Models.Req.Account;
using MyShop.Models.Resp.Account;
using MyShop.Shared.Enums;
using MyShop.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Infrastructure.Repositories.Account
{
    public sealed class AccountCreateRepository : IAccountCreateRepository
    {
        private readonly IMyShopDbContextFactory _factory;
        public AccountCreateRepository(IMyShopDbContextFactory factory) => _factory = factory;
        /// <summary>
        /// 建立會員帳號
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<MemberRegisterResp> CreateMemberAccountWithProfileAsync(MemberRegisterDto dto, CancellationToken ct = default)
        {
            var result = new MemberRegisterResp();
            using var ctx = _factory.Create(ConnectionMode.Master);
            var db = ctx.AsDbContext<MyShopContext>();
            var strategy = db.Database.CreateExecutionStrategy();

            var exists = await db.Accounts.AsNoTracking().AnyAsync(a => a.Username == dto.Username, ct);
            if (exists)
                // throw 自訂義Exception
                throw new AccountAlreadyExistsException(dto.Username);

            await strategy.ExecuteAsync(async () =>
            {
                await using var tx = await db.Database.BeginTransactionAsync(ct);
                try
                {
                    var account = new EF.Models.Account
                    {
                        Username = dto.Username,
                        PasswordHash = dto.PasswordHash,
                        Email = dto.Email,
                        Status = (int)Status.Active,
                        CreatedAt = dto.CreatedAt,
                    };
                    await db.Accounts.AddAsync(account, ct);
                    await db.SaveChangesAsync(ct);

                    var profile = new EF.Models.MemberProfile
                    {
                        AccountId = account.AccountId,
                        DisplayName = dto.Username,
                        Phone = dto.Phone,
                        Status = (int)Status.Active,
                        CreatedAt = dto.CreatedAt,
                    };
                    await db.MemberProfiles.AddAsync(profile, ct);
                    await db.SaveChangesAsync(ct);

                    var accountRole = new EF.Models.AccountRole
                    {
                        AccountId = account.AccountId,
                        RoleId = (int)UserRole.Member,
                        CreatedAt = dto.CreatedAt,
                    };
                    await db.AccountRoles.AddAsync(accountRole, ct);
                    await db.SaveChangesAsync(ct);

                    await tx.CommitAsync(ct);

                    result.AccountId = account.AccountId;
                    result.Username = account.Username;
                    result.CreatedAt = account.CreatedAt;
                }
                catch
                {
                    await tx.RollbackAsync(ct);
                    throw;
                }
            });

            return result;
        }
        /// <summary>
        /// 建立廠商帳號
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<VendorRegisterResp> CreateVendorAccountWithProfileAsync(VendorRegisterDto dto, CancellationToken ct = default)
        {
            var result = new VendorRegisterResp();
            using var ctx = _factory.Create(ConnectionMode.Master);
            var db = ctx.AsDbContext<MyShopContext>();
            var strategy = db.Database.CreateExecutionStrategy();

            var exists = await db.Accounts.AsNoTracking().AnyAsync(a => a.Username == dto.Username, ct);
            if (exists)
                // throw 自訂義Exception
                throw new AccountAlreadyExistsException(dto.Username);

            await strategy.ExecuteAsync(async () =>
            {
                await using var tx = await db.Database.BeginTransactionAsync(ct);
                try
                {
                    var account = new EF.Models.Account
                    {
                        Username = dto.Username,
                        PasswordHash = dto.PasswordHash,
                        Email = dto.Email,
                        Status = (int)Status.Active,
                        CreatedAt = dto.CreatedAt,
                    };
                    await db.Accounts.AddAsync(account, ct);
                    await db.SaveChangesAsync(ct);

                    var profile = new EF.Models.VendorProfile
                    {
                        AccountId = account.AccountId,
                        ReviewStatus = (int)Status.Disabled,
                        CreatedAt = dto.CreatedAt,
                    };
                    await db.VendorProfiles.AddAsync(profile, ct);
                    await db.SaveChangesAsync(ct);

                    var accountRole = new EF.Models.AccountRole
                    {
                        AccountId = account.AccountId,
                        RoleId = (int)UserRole.Vendor,
                        CreatedAt = dto.CreatedAt,
                    };
                    await db.AccountRoles.AddAsync(accountRole, ct);
                    await db.SaveChangesAsync(ct);

                    await tx.CommitAsync(ct);

                    result.AccountId = account.AccountId;
                    result.Username = account.Username;
                    result.CreatedAt = account.CreatedAt;
                }
                catch
                {
                    await tx.RollbackAsync(ct);
                    throw;
                }
            });

            return result;
        }
        /// <summary>
        /// 建立後台管理員帳號(步驟一)
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<long> StepCreateBasicAdminAccountAsync(IMyShopDbContext ctx, AdminCreateDto dto, CancellationToken ct = default)
        {
            var db = ctx.AsDbContext<MyShopContext>();
            var exists = await db.Accounts.AsNoTracking().AnyAsync(a => a.Username == dto.Username, ct);
            if (exists)
                // throw 自訂義Exception
                throw new AccountAlreadyExistsException(dto.Username);
            var account = new EF.Models.Account
            {
                Username = dto.Username,
                PasswordHash = dto.PasswordHash,
                Email = dto.Email,
                Status = (int)Status.Active,
                CreatedAt = dto.CreatedAt,
                CreatedBy = dto.CreatedBy,
            };
            await db.Accounts.AddAsync(account, ct);
            await ctx.SaveChangesAsync(ct);

            var accountRole = new EF.Models.AccountRole
            {
                AccountId = account.AccountId,
                RoleId = (int)UserRole.Admin,
                CreatedAt = dto.CreatedAt,
            };
            await db.AccountRoles.AddAsync(accountRole, ct);
            await ctx.SaveChangesAsync(ct);

            return account.AccountId;
        }
    }
}
