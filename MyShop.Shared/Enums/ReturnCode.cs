using System;
using System.ComponentModel;

namespace MyShop.Shared.Enums
{
    public enum ReturnCode
    {
        // ===== 成功類 =====
        Succeeded = 200,

        // ===== 查無資料 =====
        DataNotFound = 404,

        // ===== 驗證 / 登入相關 =====
        AccountNotFound = 1001,
        InvalidPassword = 1002,
        AccountLocked = 1003,

        // ===== 權限相關 =====
        PermissionDenied = 2001,

        // ===== 角色相關 =====
        UserRoleNotFound = 3001,

        // ===== 系統錯誤 =====
        ExceptionError = 500,
    }
}



