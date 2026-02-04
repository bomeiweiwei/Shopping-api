using MyShop.Models.Dto.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application.Identity
{
    public interface ICurrentUserAccessor
    {
        bool IsAuthenticated { get; }
        JwtUserInfo Current { get; } // 未登入丟 Unauthorized
    }
}
