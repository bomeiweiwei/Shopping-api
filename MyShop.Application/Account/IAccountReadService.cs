using MyShop.Models;
using MyShop.Models.Dto;
using MyShop.Models.Req.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application.Account
{
    public interface IAccountReadService
    {
        /// <summary>
        /// 取得登入的帳號資料
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<AccountDto?> GetLoginAccountData(GetAccountReq req, CancellationToken ct = default);
        /// <summary>
        /// 取得使用者資料
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<ApiResponseBase<AccountDetailDto>> GetAccountData(GetAccountReq req, CancellationToken ct = default);
    }
}
