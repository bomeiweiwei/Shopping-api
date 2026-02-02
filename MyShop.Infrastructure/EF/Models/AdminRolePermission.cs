using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class AdminRolePermission
{
    public long AdminRoleId { get; set; }

    public long PermissionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public virtual AdminRole AdminRole { get; set; } = null!;

    public virtual Permission Permission { get; set; } = null!;
}
