using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class AdminAccountAdminRole
{
    public long AdminAccountId { get; set; }

    public long AdminRoleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public virtual AdminAccount AdminAccount { get; set; } = null!;

    public virtual AdminRole AdminRole { get; set; } = null!;
}
