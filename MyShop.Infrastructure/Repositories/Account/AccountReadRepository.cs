using Microsoft.EntityFrameworkCore;
using MyShop.Domain;
using MyShop.Domain.Account;
using MyShop.Infrastructure.EF;
using MyShop.Infrastructure.EF.Data;
using MyShop.Models;
using MyShop.Models.Dto;
using MyShop.Models.Req.Account;
using MyShop.Models.Req.Identity;
using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Infrastructure.Repositories.Account
{
    public sealed class AccountReadRepository : IAccountReadRepository
    {
        private readonly IMyShopDbContextFactory _factory;
        public AccountReadRepository(IMyShopDbContextFactory factory) => _factory = factory;
        /// <summary>
        /// 取得登入的帳號資料
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<AccountDto?> GetLoginAccountData(GetAccountReq req, CancellationToken ct = default)
        {
            var result = new AccountDto();
            using var ctx = _factory.Create(ConnectionMode.Slave);
            var db = ctx.AsDbContext<MyShopContext>();

            var account = await db.Accounts.FirstOrDefaultAsync(a => a.Username == req.UserName, ct);
            if (account != null)
            {
                result.AccountId = account.AccountId;
                result.UserName = account.Username;
                result.PasswordHash = account.PasswordHash;

                return result;
            }

            return null;
        }

        /// <summary>
        /// 取得使用者資料
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<AccountDetailDto> GetAccountData(GetAccountReq req, CancellationToken ct = default)
        {
            var result = new AccountDetailDto();
            using var ctx = _factory.Create(ConnectionMode.Slave);
            var db = ctx.AsDbContext<MyShopContext>();

            var account = await db.Accounts.FirstOrDefaultAsync(a => a.AccountId == req.AccountId && a.Username == req.UserName, ct);
            if (account != null)
            {
                result.AccountId = account.AccountId;
                result.UserName = account.Username;

                result.Email = account.Email;
                result.Status = account.Status;
                result.LastLoginAt = account.LastLoginAt;
            }
            return result;
        }
    }
}
