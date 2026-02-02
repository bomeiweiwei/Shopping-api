using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyShop.Application.Account;
using MyShop.Application.Redis;
using MyShop.Domain;
using MyShop.Domain.Account;
using MyShop.Models;
using MyShop.Models.Dto.Account;
using MyShop.Models.Dto.Identity;
using MyShop.Models.Req.Account;
using MyShop.Models.Req.Identity;
using MyShop.Models.Resp.Identity;
using MyShop.Shared.Enums;
using MyShop.Shared.SysConfigs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyShop.Application.Identity.implement
{
    public class IdentityService : BaseService, IIdentityService
    {
        private readonly IPasswordHasher<AccountDto> _hasher;
        private readonly IAccountReadService _accountReadService;
        private readonly IRedisService _redis;
        public IdentityService(IMyShopDbContextFactory factory, IPasswordHasher<AccountDto> hasher, IAccountReadService accountReadService, IRedisService redis) : base(factory)
        {
            _hasher = hasher;
            _accountReadService = accountReadService;
            _redis = redis;
        }
        /// <summary>
        /// 檢查是否能登入
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<ApiResponseBase<LoginResp>> ChkLogin(LoginReq req, CancellationToken ct = default)
        {
            var result = new ApiResponseBase<LoginResp>()
            {
                Data = new LoginResp()
                {
                    IsLoginSuccess = false
                },
            };

            GetAccountReq getAccountReq = new GetAccountReq()
            {
                UserName = req.UserName
            };
            var account = await _accountReadService.GetLoginAccountData(getAccountReq, ct);
            if (account == null)
                return result;

            var verify = _hasher.VerifyHashedPassword(account, account.PasswordHash, req.Password);
            if (verify == PasswordVerificationResult.Failed)
            {
                return result;
            }

            result.Data.IsLoginSuccess = true;
            //var detail = await _accountReadService.GetAccountData(new GetAccountReq() { AccountId = account.AccountId, UserName = account.UserName });

            return result;
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
                    IsLoginSuccess = false
                },
            };

            GetAccountReq getAccountReq = new GetAccountReq()
            {
                UserName = req.UserName
            };
            var account = await _accountReadService.GetLoginAccountData(getAccountReq, ct);
            if (account == null)
                return result;

            var verify = _hasher.VerifyHashedPassword(account, account.PasswordHash, req.Password);
            if (verify == PasswordVerificationResult.Failed)
            {
                return result;
            }

            result.Data.IsLoginSuccess = true;

            DateTime expirationTime = DateTime.UtcNow.AddMinutes(30);
            TimeSpan ttl = expirationTime - DateTime.UtcNow;
            JwtUserInfo userInfo = new JwtUserInfo()
            {
                AccountId = account.AccountId,
                UserName = account.UserName,
                Expiration = expirationTime
            };
            var token = GenerateJwtToken(userInfo);

            //存redis
            await _redis.SetStringAsync($"Login:{account.AccountId}", token, ttl);
            result.Data.JwtToken = token;

            return result;
        }

        private string GenerateJwtToken(JwtUserInfo userInfo)
        {
            var claims = new List<Claim>
            {
                new Claim("AccountId", userInfo.AccountId.ToString()),
                new Claim("UserName", userInfo.UserName),
                new Claim("Expiration", userInfo.Expiration.ToString("o"))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigManager.Jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: ConfigManager.Jwt.Issuer,
                audience: ConfigManager.Jwt.Audience,
                claims: claims,
                expires: userInfo.Expiration,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
