using MyShop.Models.Req.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models.Dto.Account
{
    public class AdminCreateDto : AccountRegisterReq
    {
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }
        public List<long> RoleIds { get; set; } = new List<long>();
    }
}
