using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models.Resp.Account
{
    public class AdminCreateResp
    {
        public long AdminAccountId { get; set; }
        public string Username { get; set; }
        public UserRole Role { get; set; } = UserRole.Admin;
        public DateTime CreatedAt { get; set; }
    }
}
