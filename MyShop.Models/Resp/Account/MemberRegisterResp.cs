using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;

namespace MyShop.Models.Resp.Account
{
    public class MemberRegisterResp
    {
        public long AccountId { get; set; }
        public string Username { get; set; }
        public UserRole Role { get; set; } = UserRole.Member;
        public DateTime CreatedAt { get; set; }
    }
}
