using Microsoft.AspNetCore.Http;
using MyShop.Domain;
using MyShop.Models;
using MyShop.Models.Req.Identity;
using MyShop.Models.Resp.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application.Identity.implement
{
    public class LoginManagerService : BaseService, ILoginManagerService
    {
        private readonly IEnumerable<ILoginService> _loginServices;
        public LoginManagerService(IMyShopDbContextFactory factory, ICurrentUserAccessor currentUser, IEnumerable<ILoginService> loginServices) : base(factory, currentUser)
        {
            _loginServices = loginServices;
        }
        /// <summary>
        /// 使用者登入
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<ApiResponseBase<LoginResp>> UserLogin(LoginReq req, CancellationToken ct = default)
        {
            var result = new ApiResponseBase<LoginResp>();
            //var roles = string.Join(", ", _loginServices.Select(s => s.userRole));
            var service = _loginServices.FirstOrDefault(x => x.userRole == req.userRole);
            if (service == null)
            {
                throw new InvalidOperationException($"No login service found for type {req.userRole}");
            }
            else
            {
                result = await service.Login(req, ct);
                return result;
            }
        }
    }
}
