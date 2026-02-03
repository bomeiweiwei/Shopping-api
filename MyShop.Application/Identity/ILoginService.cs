using MyShop.Models;
using MyShop.Models.Req.Identity;
using MyShop.Models.Resp.Identity;
using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application.Identity
{
    public interface ILoginService
    {
        UserRole userRole { get; }
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<ApiResponseBase<LoginResp>> Login(LoginReq req, CancellationToken ct = default);
    }
}
