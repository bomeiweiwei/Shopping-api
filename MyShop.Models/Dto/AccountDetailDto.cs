using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models.Dto
{
    public class AccountDetailDto: AccountDto
    {
        public string Email { get; set; }
        public int Status { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }
}
