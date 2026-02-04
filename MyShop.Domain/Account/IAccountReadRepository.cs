using MyShop.Models;
using MyShop.Models.Dto.Account;
using MyShop.Models.Req.Account;
using MyShop.Models.Req.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Domain.Account
{
    public interface IAccountReadRepository
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
        Task<AccountDetailDto> GetAccountData(GetAccountReq req, CancellationToken ct = default);
    }
}
