using MyShop.Models;
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
        Task<ApiResponseBase<MemberRegisterResp>> CreateMemberAccountWithProfileAsync(MemberRegisterReq req, CancellationToken ct = default);
    }
}
