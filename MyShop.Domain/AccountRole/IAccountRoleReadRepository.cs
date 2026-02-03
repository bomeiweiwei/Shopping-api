using MyShop.Models.Dto.Account;
using MyShop.Models.Dto.AccountRole;
using MyShop.Models.Req.Account;
using MyShop.Models.Req.AccountRole;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Domain.AccountRole
{
    public interface IAccountRoleReadRepository
    {
        Task<AccountRoleDto?> GetAccountRoleData(GetAccountRoleReq req, CancellationToken ct = default);
    }
}
