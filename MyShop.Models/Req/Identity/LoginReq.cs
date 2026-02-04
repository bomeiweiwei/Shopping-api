using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyShop.Models.Req.Identity
{
    public class LoginReq
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public UserRole userRole { get; set; } = UserRole.Member;
    }
}
