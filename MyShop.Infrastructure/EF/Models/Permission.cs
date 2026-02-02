using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class Permission
{
    public long PermissionId { get; set; }

    public string PermissionCode { get; set; } = null!;

    public string? PermissionName { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public virtual ICollection<AdminRolePermission> AdminRolePermissions { get; set; } = new List<AdminRolePermission>();
}
