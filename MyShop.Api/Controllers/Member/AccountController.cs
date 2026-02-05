using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Api.Attributes;
using MyShop.Api.Filters;
using MyShop.Shared.Enums;

namespace MyShop.Api.Controllers.Member
{
    [Route("api/member/[controller]")]
    [ApiController]
    [JwtAuthActionFilter]
    [RoleAuthorize(UserRole.Admin, UserRole.Member)]
    public class AccountController : ControllerBase
    {
       
    }
}
