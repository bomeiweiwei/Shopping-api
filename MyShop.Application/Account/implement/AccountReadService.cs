using MyShop.Application.Identity;
using MyShop.Domain;
using MyShop.Domain.Account;
using MyShop.Models;
using MyShop.Models.Dto.Account;
using MyShop.Models.Req.Account;
using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application.Account.implement
{
    public class AccountReadService : BaseService, IAccountReadService
    {
        private readonly IAccountReadRepository _repo;
        public AccountReadService(IMyShopDbContextFactory factory, ICurrentUserAccessor currentUser, IAccountReadRepository repo) : base(factory, currentUser)
        {
            _repo = repo;
        }
        /// <summary>
        /// 取得登入的帳號資料
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<AccountDto?> GetLoginAccountData(GetAccountReq req, CancellationToken ct = default)
        {
            var result = await _repo.GetLoginAccountData(req, ct);
            return result;
        }
        /// <summary>
        /// 取得使用者資料
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<ApiResponseBase<AccountDetailDto>> GetAccountData(GetAccountReq req, CancellationToken ct = default)
        {
            var result = new ApiResponseBase<AccountDetailDto>();
            var data = await _repo.GetAccountData(req, ct);
            if (data != null)
            {
                result.Data = data;
            }
            else
            {
                result.StatusCode = (long)ReturnCode.DataNotFound;
            }
            return result;
        }
    }
}
