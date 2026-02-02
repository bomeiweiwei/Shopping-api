using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models.Dto
{
    public class AccountDto
    {
        public long AccountId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
