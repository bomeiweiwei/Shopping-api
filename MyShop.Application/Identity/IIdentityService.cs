using MyShop.Models;
using MyShop.Models.Dto.Identity;
using MyShop.Models.Req.Identity;
using MyShop.Models.Resp.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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
        /// <summary>
        /// 驗證登入資料
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<VerifyLogin> VerifyLoginData(LoginReq req, CancellationToken ct = default);
        /// <summary>
        /// 取得JwtToken
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        Task<string> GetJwtToken(List<Claim> claims, DateTime expiration);
    }
}
