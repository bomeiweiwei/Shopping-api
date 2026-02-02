using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class FavoriteStore
{
    public long AccountId { get; set; }

    public long StoreId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
