using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Identity;
using MyShop.Application.Test;
using MyShop.Application.Test.implement;
using MyShop.Models.Req.Identity;

namespace MyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _service;
        private readonly ILoginManagerService _loginManagerService;
        public IdentityController(IIdentityService service, ILoginManagerService loginManagerService)
        {
            _service = service;
            _loginManagerService = loginManagerService;
        }
        /// <summary>
        /// 檢查是否能登入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("ChkLogin")]
        //public async Task<IActionResult> ChkLogin(LoginReq req)
        //{
        //    var result = await _service.ChkLogin(req);
        //    return Ok(new { result });
        //}

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginReq req)
        {
            var result = await _loginManagerService.UserLogin(req);
            return Ok(new { result });
        }
    }
}
