using MyShop.Application.Redis;
using MyShop.Domain;
using MyShop.Domain.AccountRole;
using MyShop.Domain.AdminPermission;
using MyShop.Models;
using MyShop.Models.Dto.Identity;
using MyShop.Models.Req.AccountRole;
using MyShop.Models.Req.AdminPermission;
using MyShop.Models.Req.Identity;
using MyShop.Models.Resp.Identity;
using MyShop.Shared.Enums;
using MyShop.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MyShop.Application.Identity.implement
{
    public class AdminUserLoginService : BaseService, ILoginService
    {
        public UserRole userRole => UserRole.Admin;

        private readonly IIdentityService _identityService;
        private readonly IRedisService _redis;
        private readonly IAccountRoleReadRepository _accountRoleReadRepository;
        private readonly IAdminPermissionReadRepository _adminPermissionReadRepository;
        public AdminUserLoginService(IMyShopDbContextFactory factory, ICurrentUserAccessor currentUser, IIdentityService identityService, IRedisService redis, IAccountRoleReadRepository accountRoleReadRepository, IAdminPermissionReadRepository adminPermissionReadRepository) : base(factory, currentUser)
        {
            _identityService = identityService;
            _redis = redis;
            _accountRoleReadRepository = accountRoleReadRepository;
            _adminPermissionReadRepository = adminPermissionReadRepository;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<ApiResponseBase<LoginResp>> Login(LoginReq req, CancellationToken ct = default)
        {
            var result = new ApiResponseBase<LoginResp>()
            {
                Data = new LoginResp()
                {
                    IsLoginSuccess = false,
                    JwtToken = string.Empty
                }
            };
            var verify = await _identityService.VerifyLoginData(req, ct);
            if (!verify.IsVerifySuccess)
            {
                result.StatusCode = (long)ReturnCode.AccountNotFound;
                result.Message = ReturnCode.AccountNotFound.GetDescription();
                return result;
            }
            if (verify.Data == null)
            {
                result.StatusCode = (long)ReturnCode.AccountNotFound;
                result.Message = ReturnCode.AccountNotFound.GetDescription();
                return result;
            }
            // 取得帳號資料
            var account = verify.Data;
            int roleId = (int)userRole;
            GetAccountRoleReq getAccountRoleReq = new GetAccountRoleReq()
            {
                AccountId = account.AccountId,
                RoleId = roleId
            };
            var accountRoleDatas = await _accountRoleReadRepository.GetAccountRolesData(getAccountRoleReq, ct);
            var chkRoleData= accountRoleDatas.Where(x => x.RoleId == roleId).FirstOrDefault();
            if (chkRoleData == null)
            {
                result.StatusCode = (long)ReturnCode.UserRoleNotFound;
                result.Message = ReturnCode.UserRoleNotFound.GetDescription();
                return result;
            }
            GetAdminPermissionsDataReq getAdminPermissionsDataReq = new GetAdminPermissionsDataReq()
            {
                AccountId = account.AccountId
            };
           var permissions = await _adminPermissionReadRepository.GetAdminPermissionsData(getAdminPermissionsDataReq, ct);

            // 設定時間
            DateTime expirationTime = DateTime.UtcNow.AddDays(1);
            TimeSpan ttl = expirationTime - DateTime.UtcNow;
            // 設定JwtUserInfo
            //JwtUserInfo userInfo = new JwtUserInfo()
            //{
            //    AccountId = account.AccountId,
            //    UserName = account.UserName,
            //    AccountRole = (int)userRole,
            //    Expiration = expirationTime
            //};
            // 設定Claims
            var claims = new List<Claim>
            {
                new Claim("AccountId", account.AccountId.ToString()),
                new Claim("UserName", account.UserName),
                new Claim("PrimaryUserRole", roleId.ToString()),
                new Claim("Expiration",expirationTime.ToString("o"))
            };
            foreach (var permission in permissions)
            {
                claims.Add(new Claim("Permission", permission.PermissionCode.ToString()));
            }
            foreach (var role in accountRoleDatas)
            {
                claims.Add(new Claim("UserRole", role.RoleId.ToString()));
            }
            // 產生Token
            var token = await _identityService.GetJwtToken(claims, expirationTime);

            //存redis
            await _redis.SetStringAsync($"Login:{account.AccountId}", token, ttl);

            result.Data.IsLoginSuccess = true;
            result.Data.JwtToken = token;

            return result;
        }
    }
}
