using MyShop.Models;
using MyShop.Models.Req.Identity;
using MyShop.Models.Resp.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application.Identity
{
    /// <summary>
    /// 統一對外提供的「登入入口」
    /// </summary>
    public interface ILoginManagerService
    {
        /// <summary>
        /// 使用者登入
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<ApiResponseBase<LoginResp>> UserLogin(LoginReq req, CancellationToken ct = default);
    }
}
