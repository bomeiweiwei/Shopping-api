using MyShop.Models.Dto.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Text;

namespace MyShop.Application.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static JwtUserInfo? ToJwtUserInfo(this ClaimsPrincipal? user)
        {
            if (user?.Identity?.IsAuthenticated != true) return null;

            string? accountIdStr = user.FindFirst("AccountId")?.Value;
            string? userName = user.FindFirst("UserName")?.Value;
            string? primaryRoleStr = user.FindFirst("PrimaryUserRole")?.Value;
            string? expStr = user.FindFirst("Expiration")?.Value;

            if (string.IsNullOrWhiteSpace(accountIdStr) ||
                string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(primaryRoleStr) ||
                string.IsNullOrWhiteSpace(expStr))
                return null;

            var roles = user.FindAll("UserRole")
                .Select(c => TryParseInt(c.Value))
                .Where(v => v.HasValue)
                .Select(v => v!.Value)
                .ToHashSet();

            var permissions = user.FindAll("Permission")
                .Select(c => TryParseInt(c.Value))
                .Where(v => v.HasValue)
                .Select(v => v!.Value)
                .ToHashSet();

            return new JwtUserInfo
            {
                AccountId = long.Parse(accountIdStr, CultureInfo.InvariantCulture),
                UserName = userName,
                PrimaryUserRole = int.Parse(primaryRoleStr, CultureInfo.InvariantCulture),
                UserRoles = roles,
                Permissions = permissions,
                Expiration = DateTime.Parse(expStr, null, DateTimeStyles.RoundtripKind),
            };
        }

        private static int? TryParseInt(string? s)
            => int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var v) ? v : null;
    }
}
