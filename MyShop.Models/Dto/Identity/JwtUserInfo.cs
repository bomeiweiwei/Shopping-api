using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models.Dto.Identity
{
    public class JwtUserInfo
    {
        public long AccountId { get; set; }
        public string UserName { get; set; }
        public DateTime Expiration { get; set; }
    }
}
