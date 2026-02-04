using MyShop.Models.Dto.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models.Dto.Identity
{
    public class VerifyLogin
    {
        /// <summary>
        /// 是否驗證成功
        /// </summary>
        public bool IsVerifySuccess  { get; set; }
        /// <summary>
        /// 資料
        /// </summary>
        public AccountDto? Data { get; set; }
    }
}
