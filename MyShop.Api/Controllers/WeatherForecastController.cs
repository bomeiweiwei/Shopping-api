using Microsoft.AspNetCore.Mvc;
using MyShop.Api.Attributes;
using MyShop.Api.Filters;
using MyShop.Shared.Enums;

namespace MyShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [JwtAuthActionFilter]
    [RoleAuthorize(UserRole.Admin)]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet(Name = "GetWeatherForecast")]
        [PermissionAuthorize(PermissionCode.Account_Read, PermissionCode.Account_Create)]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
