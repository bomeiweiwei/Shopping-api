using Microsoft.AspNetCore.Identity;
using MyShop.Application.Account;
using MyShop.Domain;
using MyShop.Domain.Account;
using MyShop.Models;
using MyShop.Models.Dto;
using MyShop.Models.Req.Account;
using MyShop.Models.Req.Identity;
using MyShop.Models.Resp.Identity;
using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application.Identity.implement
{
    public class IdentityService : BaseService, IIdentityService
    {
        private readonly IPasswordHasher<AccountDto> _hasher;
        private readonly IAccountReadService _accountReadService;
        public IdentityService(IMyShopDbContextFactory factory, IPasswordHasher<AccountDto> hasher, IAccountReadService accountReadService) : base(factory)
        {
            _hasher = hasher;
            _accountReadService = accountReadService;
        }
        /// <summary>
        /// 檢查是否能登入
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<ApiResponseBase<LoginResp>> ChkLogin(LoginReq req, CancellationToken ct = default)
        {
            var result = new ApiResponseBase<LoginResp>()
            {
                Data = new LoginResp()
                {
                    IsLoginSuccess = false
                },
            };

            GetAccountReq getAccountReq = new GetAccountReq()
            {
                UserName = req.UserName
            };
            var account = await _accountReadService.GetLoginAccountData(getAccountReq);
            if (account == null)
                return result;

            var verify = _hasher.VerifyHashedPassword(account, account.PasswordHash, req.Password);
            if (verify == PasswordVerificationResult.Failed)
            {
                return result;
            }

            result.Data.IsLoginSuccess = true;
            //var detail = await _accountReadService.GetAccountData(new GetAccountReq() { AccountId = account.AccountId, UserName = account.UserName });

            return result;
        }
    }
}
