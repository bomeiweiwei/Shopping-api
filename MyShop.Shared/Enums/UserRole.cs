using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MyShop.Shared.Enums
{
    public enum UserRole
    {
        /// <summary>
        /// 系統管理者
        /// </summary>
        [Description("系統管理者")]
        Admin = 1,
        /// <summary>
        /// 廠商
        /// </summary>
        [Description("廠商")]
        Vendor = 2,
        /// <summary>
        /// 會員
        /// </summary>
        [Description("會員")]
        Member = 3,
    }
}
