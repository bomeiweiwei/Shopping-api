using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class AdminAccount
{
    public long AdminAccountId { get; set; }

    public long AccountId { get; set; }

    public string? DisplayName { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<AdminAccountAdminRole> AdminAccountAdminRoles { get; set; } = new List<AdminAccountAdminRole>();
}
