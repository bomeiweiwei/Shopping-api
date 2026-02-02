using MyShop.Models;
using MyShop.Models.Req.Identity;
using MyShop.Models.Resp.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application.Identity
{
    public interface IIdentityService
    {
        /// <summary>
        /// 檢查是否能登入
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<ApiResponseBase<LoginResp>> ChkLogin(LoginReq req, CancellationToken ct = default);
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<ApiResponseBase<LoginResp>> Login(LoginReq req, CancellationToken ct = default);
    }
}
