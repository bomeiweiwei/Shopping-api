using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Api.Attributes;
using MyShop.Api.Filters;
using MyShop.Shared.Enums;

namespace MyShop.Api.Controllers.Vendor
{
    [Route("api/vendor/[controller]")]
    [ApiController]
    [JwtAuthActionFilter]
    [RoleAuthorize(UserRole.Admin, UserRole.Vendor)]
    public class AccountController : ControllerBase
    {
    }
}
