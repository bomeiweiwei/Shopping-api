using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Identity;
using MyShop.Application.Test;
using MyShop.Application.Test.implement;
using MyShop.Models;
using MyShop.Models.Req.Identity;
using MyShop.Models.Resp.Identity;
using MyShop.Shared.Enums;
using MyShop.Shared.Mapper;

namespace MyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly ILoginManagerService _loginManagerService;
        public IdentityController(ILoginManagerService loginManagerService)
        {
            _loginManagerService = loginManagerService;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponseBase<LoginResp>>> Login([FromBody] LoginReq req, CancellationToken ct)
        {
            var result = await _loginManagerService.UserLogin(req, ct);

            var httpCode = ((ReturnCode)result.StatusCode).ToHttpStatusCode();
            return StatusCode(httpCode, result);
        }
    }
}
