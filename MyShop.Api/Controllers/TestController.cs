using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Test;

namespace MyShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ITestService _testService;
        public TestController(IWebHostEnvironment env, ITestService testService)
        {
            _env = env;
            _testService = testService;
        }
        /// <summary>
        /// 取得環境變數
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEnv")]
        public async Task<IActionResult> GetEnv()
        {
            var environment = _env.EnvironmentName;
            return Ok(new { environment });
        }
        /// <summary>
        /// 檢查資料庫連線
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetConnectResult")]
        public async Task<IActionResult> GetConnectResult()
        {
            var connectResult = await _testService.GetConnectResult();
            return Ok(new { connectResult });
        }
    }
}
