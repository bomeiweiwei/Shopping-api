using Microsoft.AspNetCore.Http;
using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Shared.Mapper
{
    public static class ReturnCodeHttpMapper
    {
        public static int ToHttpStatusCode(this ReturnCode code)
        {
            return code switch
            {
                ReturnCode.Succeeded => StatusCodes.Status200OK,
                ReturnCode.DataNotFound => StatusCodes.Status404NotFound,

                // 登入 / 驗證
                ReturnCode.AccountNotFound => StatusCodes.Status404NotFound,
                ReturnCode.InvalidPassword => StatusCodes.Status401Unauthorized,
                ReturnCode.AccountLocked => StatusCodes.Status403Forbidden,

                // 權限
                ReturnCode.PermissionDenied => StatusCodes.Status403Forbidden,

                // 角色
                ReturnCode.UserRoleNotFound => StatusCodes.Status404NotFound,

                // 系統錯誤
                ReturnCode.ExceptionError => StatusCodes.Status500InternalServerError,

                _ => StatusCodes.Status500InternalServerError
            };
        }
    }

}
