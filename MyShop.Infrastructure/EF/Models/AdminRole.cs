using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class AdminRole
{
    public long AdminRoleId { get; set; }

    public string RoleCode { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public virtual ICollection<AdminAccountAdminRole> AdminAccountAdminRoles { get; set; } = new List<AdminAccountAdminRole>();

    public virtual ICollection<AdminRolePermission> AdminRolePermissions { get; set; } = new List<AdminRolePermission>();
}
