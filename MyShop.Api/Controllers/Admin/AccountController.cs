using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Api.Attributes;
using MyShop.Api.Filters;
using MyShop.Shared.Enums;

namespace MyShop.Api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [JwtAuthActionFilter]
    [RoleAuthorize(UserRole.Admin)]
    public class AccountController : ControllerBase
    {
    }
}
