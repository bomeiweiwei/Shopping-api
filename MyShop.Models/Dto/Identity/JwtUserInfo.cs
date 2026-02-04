using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models.Dto.Identity
{
    public class JwtUserInfo
    {
        public long AccountId { get; init; }
        public string UserName { get; init; } = "";

        public int PrimaryUserRole { get; init; }            // 本次登入入口角色
        public HashSet<int> UserRoles { get; init; } = new();// 帳號擁有角色（可能多）
        public HashSet<int> Permissions { get; init; } = new(); // Admin 才會有（可能空）

        public DateTime Expiration { get; init; }

        public bool IsAdminLogin => PrimaryUserRole == (int)UserRole.Admin;
    }
}
