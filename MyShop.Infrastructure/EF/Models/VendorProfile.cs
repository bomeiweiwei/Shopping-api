using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class VendorProfile
{
    public long VendorId { get; set; }

    public long AccountId { get; set; }

    public int ReviewStatus { get; set; }

    public string? ReviewComment { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
