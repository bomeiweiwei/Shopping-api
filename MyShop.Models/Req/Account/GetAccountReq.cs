using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models.Req.Account
{
    public class GetAccountReq
    {
        public long AccountId { get; set; }
        public string UserName { get; set; }
    }
}
