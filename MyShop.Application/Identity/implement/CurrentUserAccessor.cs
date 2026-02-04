using Microsoft.AspNetCore.Http;
using MyShop.Application.Extensions;
using MyShop.Models.Dto.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application.Identity.implement
{
    public sealed class CurrentUserAccessor : ICurrentUserAccessor
    {
        private readonly IHttpContextAccessor _http;

        public CurrentUserAccessor(IHttpContextAccessor http)
        {
            _http = http;
        }

        public bool IsAuthenticated =>
            _http.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public JwtUserInfo Current =>
            _http.HttpContext?.User?.ToJwtUserInfo()
            ?? throw new UnauthorizedAccessException("User is not authenticated.");
    }
}
