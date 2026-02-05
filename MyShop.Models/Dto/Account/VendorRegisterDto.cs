using MyShop.Models.Req.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models.Dto.Account
{
    public class VendorRegisterDto : AccountRegisterReq
    {
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
