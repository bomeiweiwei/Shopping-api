using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MyShop.Shared.Enums
{
    public enum PermissionCode
    {
        [Description("查看帳號")]
        Account_Read = 1000,
        [Description("新增帳號")]
        Account_Create = 1001,
        [Description("修改帳號")]
        Account_Update = 1002,
        [Description("刪除帳號")]
        Account_Delete = 1003,
    }
}
