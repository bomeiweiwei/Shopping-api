using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models.Req.Account
{
    public class AdminAccountRegisterReq: AccountRegisterReq
    {
        public List<long> RoleIds { get; set; } = new List<long>();
    }
}
