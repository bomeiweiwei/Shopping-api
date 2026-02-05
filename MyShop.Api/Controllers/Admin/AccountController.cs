using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Api.Attributes;
using MyShop.Api.Filters;
using MyShop.Application.Account;
using MyShop.Models;
using MyShop.Models.Req.Account;
using MyShop.Models.Resp.Account;
using MyShop.Shared.Enums;
using MyShop.Shared.Mapper;

namespace MyShop.Api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [JwtAuthActionFilter]
    [RoleAuthorize(UserRole.Admin)]
    public class AccountController : ControllerBase
    {
        private readonly IAccountCreateService _accountCreateService;
        public AccountController(IAccountCreateService accountCreateService)
        {
            _accountCreateService = accountCreateService;
        }
        /// <summary>
        /// 建立後台管理員帳號
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionCode.Account_Create)]
        public async Task<ActionResult<ApiResponseBase<MemberRegisterResp>>> Create([FromBody] AdminAccountRegisterReq req, CancellationToken ct)
        {
            var result = await _accountCreateService.CreateAdminAccountAsync(req, ct);

            var httpCode = ((ReturnCode)result.StatusCode).ToHttpStatusCode();
            return StatusCode(httpCode, result);
        }
    }
}
