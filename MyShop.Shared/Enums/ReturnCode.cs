using System;
using System.ComponentModel;

namespace MyShop.Shared.Enums
{
    public enum ReturnCode
    {
        // ===== 成功類 =====
        [Description("成功")]
        Succeeded = 200,

        // ===== 查無資料 =====
        [Description("查無資料")]
        DataNotFound = 404,

        // ===== 驗證 / 登入相關 =====
        [Description("帳號不存在")]
        AccountNotFound = 1001,
        [Description("密碼錯誤")]
        InvalidPassword = 1002,
        [Description("帳號被鎖定")]
        AccountLocked = 1003,

        // ===== 權限相關 =====
        [Description("無權限")]
        PermissionDenied = 2001,

        // ===== 角色相關 =====
        [Description("角色不存在")]
        UserRoleNotFound = 3001,

        // ===== 系統錯誤 =====
        [Description("系統錯誤")]
        ExceptionError = 500,
    }
}



