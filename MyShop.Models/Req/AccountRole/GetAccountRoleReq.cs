using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models.Req.AccountRole
{
    public class GetAccountRoleReq
    {
        public long AccountId { get; set; }
        public int RoleId { get; set; }
    }
}
