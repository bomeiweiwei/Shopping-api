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
        public async Task<MemberRegisterResp> CreateMemberAccountWithProfileAsync(MemberRegisterDto dto, CancellationToken ct = default)
        {
            var result = new MemberRegisterResp();
            using var ctx = _factory.Create(ConnectionMode.Master);
            var db = ctx.AsDbContext<MyShopContext>();
            var strategy = db.Database.CreateExecutionStrategy();

            var exists = await db.Accounts.AnyAsync(a => a.Username == dto.Username, ct);
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
                        CreatedAt = DateTime.UtcNow,
                    };
                    await db.Accounts.AddAsync(account, ct);
                    await db.SaveChangesAsync(ct);

                    var profile = new EF.Models.MemberProfile
                    {
                        AccountId = account.AccountId,
                        DisplayName = dto.Username,
                        Phone = dto.Phone,
                        Status = (int)Status.Active,
                        CreatedAt = DateTime.UtcNow,
                    };
                    await db.MemberProfiles.AddAsync(profile, ct);
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
    }
}
