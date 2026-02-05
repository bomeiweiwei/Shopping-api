using MyShop.Models.Dto.Account;
using MyShop.Models.Req.Account;
using MyShop.Models.Resp.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Domain.Account
{
    public interface IAccountCreateRepository
    {
        /// <summary>
        /// 建立會員帳號
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<MemberRegisterResp> CreateMemberAccountWithProfileAsync(MemberRegisterDto dto, CancellationToken ct = default);
        /// <summary>
        /// 建立廠商帳號
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<VendorRegisterResp> CreateVendorAccountWithProfileAsync(VendorRegisterDto dto, CancellationToken ct = default);
    }
}
