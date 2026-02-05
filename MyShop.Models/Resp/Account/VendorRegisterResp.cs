using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models.Resp.Account
{
    public class VendorRegisterResp
    {
        public long AccountId { get; set; }
        public string Username { get; set; }
        public UserRole Role { get; set; } = UserRole.Vendor;
        public DateTime CreatedAt { get; set; }
    }
}
