using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class StoreAdvertisement
{
    public long AdId { get; set; }

    public long StoreId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public DateTime? StartAt { get; set; }

    public DateTime? EndAt { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Store Store { get; set; } = null!;
}
