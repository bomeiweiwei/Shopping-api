using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class MemberProfile
{
    public long MemberId { get; set; }

    public long AccountId { get; set; }

    public string? DisplayName { get; set; }

    public string? Phone { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public virtual Account Account { get; set; } = null!;
}
