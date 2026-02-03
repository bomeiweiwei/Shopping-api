using MyShop.Models.Dto.AccountRole;
using MyShop.Models.Req.AccountRole;
using MyShop.Models.Req.AdminPermission;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Domain.AdminPermission
{
    public interface IAdminPermissionReadRepository
    {
        Task<List<long>> GetAdminPermissionsData(GetAdminPermissionsDataReq req, CancellationToken ct = default);
    }
}
