using MyShop.Application.Redis;
using MyShop.Domain;
using MyShop.Domain.AccountRole;
using MyShop.Models;
using MyShop.Models.Dto.Identity;
using MyShop.Models.Req.AccountRole;
using MyShop.Models.Req.Identity;
using MyShop.Models.Resp.Identity;
using MyShop.Shared.Enums;
using System.Security.Claims;

namespace MyShop.Application.Identity.implement
{
    public class MemberUserLoginService : BaseService, ILoginService
    {
        public UserRole userRole => UserRole.Member;

        private readonly IIdentityService _identityService;
        private readonly IRedisService _redis;
        private readonly IAccountRoleReadRepository _accountRoleReadRepository;
        public MemberUserLoginService(IMyShopDbContextFactory factory, IIdentityService identityService, IRedisService redis, IAccountRoleReadRepository accountRoleReadRepository) : base(factory)
        {
            _identityService = identityService;
            _redis = redis;
            _accountRoleReadRepository = accountRoleReadRepository;
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
                return result;
            if (verify.Data == null)
                return result;
            // 取得帳號資料
            var account = verify.Data;
            int roleId = (int)userRole;
            GetAccountRoleReq getAccountRoleReq = new GetAccountRoleReq()
            {
                AccountId = account.AccountId,
                RoleId = roleId
            };
            var chkRoleData = await _accountRoleReadRepository.GetAccountRoleData(getAccountRoleReq, ct);
            if (chkRoleData == null)
                return result;

            // 設定時間
            DateTime expirationTime = DateTime.UtcNow.AddMinutes(30);
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
                new Claim("UserRole", roleId.ToString()),
                new Claim("Expiration", expirationTime.ToString("o"))
            };
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
