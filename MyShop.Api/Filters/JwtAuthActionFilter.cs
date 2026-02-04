using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyShop.Api.Attributes;
using MyShop.Application.Redis;
using MyShop.Models.Resp.Error;
using MyShop.Shared.Enums;
using MyShop.Shared.Helper;
using MyShop.Shared.SysConfigs;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MyShop.Api.Filters
{
    public class JwtAuthActionFilter : TypeFilterAttribute
    {
        public JwtAuthActionFilter() : base(typeof(JwtAuthActionFilterImpl))
        {
        }
    }

    public class JwtAuthActionFilterImpl : IAsyncActionFilter
    {
        private readonly IConfiguration _configuration;
        private readonly IRedisService _redisService;
        public JwtAuthActionFilterImpl(IConfiguration configuration, IRedisService redisService)
        {
            _configuration = configuration;
            _redisService = redisService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Default
            bool isAllowAnonymous = false;
            int statusCode = (int)StatusCodes.Status401Unauthorized;
            string statusMessage = "未授權";
            var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                isAllowAnonymous = false;
                statusCode = (int)StatusCodes.Status401Unauthorized;
                statusMessage = "缺少或無效的授權標頭";
            }
            else
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(ConfigManager.Jwt.Key);

                try
                {
                    var principal = JwtHelper.ValidateToken(token, _configuration, out var jwtToken, out var error);
                    if (principal == null || jwtToken == null)
                    {
                        isAllowAnonymous = false;
                        statusCode = (int)StatusCodes.Status401Unauthorized;
                        statusMessage = $"Token 驗證失敗: {error}";
                    }
                    else
                    {
                        // 設定 HttpContext.User 讓後續可使用(BaseService)
                        context.HttpContext.User = principal;

                        var expUnix = long.Parse(jwtToken.Claims.First(x => x.Type == "exp").Value);
                        var expTime = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;
                        var now = DateTime.UtcNow;

                        if (expTime <= now)
                        {
                            isAllowAnonymous = false;
                            statusCode = (int)StatusCodes.Status403Forbidden;
                            statusMessage = "登入已逾期，請重新登入";
                        }
                        else
                        {
                            // 解析 Claim
                            var accountId = jwtToken.Claims.FirstOrDefault(x => x.Type == "AccountId")?.Value;
                            if (accountId != null)
                            {
                                string redisKey = $"Login:{accountId}";

                                string? redisToken = await _redisService.GetStringAsync(redisKey);
                                if (redisToken != token)
                                {
                                    isAllowAnonymous = false;
                                    statusMessage = "已有其他裝置登入，請重新登入";
                                    statusCode = (int)StatusCodes.Status409Conflict;
                                }
                                else
                                {
                                    isAllowAnonymous = true;
                                    if ((expTime - now).TotalMinutes <= 5)
                                    {
                                        // Token 快過期，前端可決定是否自動 Refresh
                                        context.HttpContext.Response.Headers.Add("X-Token-Expiring", "true");
                                    }
                                }
                            }
                            else
                            {
                                isAllowAnonymous = false;
                                statusCode = (int)StatusCodes.Status401Unauthorized;
                                statusMessage = "Token 驗證失敗";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    isAllowAnonymous = false;
                    statusCode = (int)StatusCodes.Status401Unauthorized;
                    statusMessage = "Token 驗證失敗: " + ex.Message;
                }
            }

            if (!isAllowAnonymous)
            {
                context.Result = new ObjectResult(new CustomErrorResponse(statusMessage, statusCode))
                {
                    StatusCode = statusCode
                };
                return;
            }
            else
            {
                var userRoles = context.HttpContext.User.Claims
                                 .Where(c => c.Type == "UserRole")
                                 .Select(c => int.TryParse(c.Value, out var r) ? r : (int?)null)
                                 .Where(r => r.HasValue)
                                 .Select(r => r!.Value)
                                 .ToHashSet();

                var roleAttr = context.ActionDescriptor.EndpointMetadata
                                .OfType<RoleAuthorizeAttribute>()
                                .FirstOrDefault();
                #region 角色檢查
                if (roleAttr != null)
                {
                    bool roleAllowed = userRoles.Overlaps(roleAttr.AllowedRoles);

                    if (!roleAllowed)
                    {
                        context.Result = new ObjectResult(
                            new CustomErrorResponse("您的角色無權限存取此資源", StatusCodes.Status403Forbidden))
                        {
                            StatusCode = StatusCodes.Status403Forbidden
                        };
                        return;
                    }
                }
                #endregion
                #region 當角色為Admin時，進行方法的權限檢查
                if (userRoles.Contains((int)UserRole.Admin))
                {
                    // 權限檢查開始（PermissionAuthorizeAttribute）
                    var permissionAttr = context.ActionDescriptor.EndpointMetadata
                                        .OfType<PermissionAuthorizeAttribute>()
                                        .FirstOrDefault();

                    if (permissionAttr != null)
                    {
                        var userPermissions = context.HttpContext.User.Claims
                                                .Where(c => c.Type == "Permission")
                                                .Select(c => int.Parse(c.Value))
                                                .ToHashSet();

                        bool hasPermission = permissionAttr.RequiredPermissions.Any(rp => userPermissions.Contains(rp));

                        if (!hasPermission)
                        {
                            context.Result = new ObjectResult(new CustomErrorResponse("您沒有足夠的權限執行此操作", StatusCodes.Status403Forbidden))
                            {
                                StatusCode = StatusCodes.Status403Forbidden
                            };
                            return;
                        }
                    }
                }
                #endregion

                await next(); // 驗證通過，繼續執行原始 Action
            }
        }
    }
}
