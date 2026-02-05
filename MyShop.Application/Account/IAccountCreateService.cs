using MyShop.Models;
using MyShop.Models.Dto.Account;
using MyShop.Models.Req.Account;
using MyShop.Models.Resp.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application.Account
{
    public interface IAccountCreateService
    {
        /// <summary>
        /// 建立會員帳號
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<ApiResponseBase<MemberRegisterResp>> CreateMemberAccountWithProfileAsync(AccountRegisterReq req, CancellationToken ct = default);
        /// <summary>
        /// 建立廠商帳號
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<ApiResponseBase<VendorRegisterResp>> CreateVendorAccountWithProfileAsync(AccountRegisterReq req, CancellationToken ct = default);
        /// <summary>
        /// 建立後台管理員帳號
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<ApiResponseBase<AdminCreateResp>> CreateAdminAccountAsync(AdminAccountRegisterReq req, CancellationToken ct = default);
    }
}
