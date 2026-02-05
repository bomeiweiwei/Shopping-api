using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Account;
using MyShop.Models;
using MyShop.Models.Req.Account;
using MyShop.Models.Resp.Account;
using MyShop.Shared.Enums;
using MyShop.Shared.Mapper;

namespace MyShop.Api.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountCreateService _accountCreateService;
        public AccountController(IAccountCreateService accountCreateService)
        {
            _accountCreateService = accountCreateService;
        }

        /// <summary>
        /// 會員註冊
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponseBase<MemberRegisterResp>>> MemberRegister([FromBody] AccountRegisterReq req, CancellationToken ct)
        {
            var result = await _accountCreateService.CreateMemberAccountWithProfileAsync(req, ct);

            var httpCode = ((ReturnCode)result.StatusCode).ToHttpStatusCode();
            return StatusCode(httpCode, result);
        }
        /// <summary>
        /// 廠商註冊
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost("vregister")]
        public async Task<ActionResult<ApiResponseBase<VendorRegisterResp>>> VendorRegister([FromBody] AccountRegisterReq req, CancellationToken ct)
        {
            var result = await _accountCreateService.CreateVendorAccountWithProfileAsync(req, ct);

            var httpCode = ((ReturnCode)result.StatusCode).ToHttpStatusCode();
            return StatusCode(httpCode, result);
        }
    }
}
