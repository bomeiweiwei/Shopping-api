using MyShop.Models.Dto.Account;
using MyShop.Models.Resp.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Domain.Account
{
    public interface IAdminAccountCreateRepository
    {
        /// <summary>
        /// 建立後台管理員帳號(步驟二)
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="dto"></param>
        /// <param name="accountId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<AdminCreateResp> StepCreateAdminAccountAsync(IMyShopDbContext ctx, AdminCreateDto dto, long accountId, CancellationToken ct = default);
    }
}
