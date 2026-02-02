using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class HotStore
{
    public long StoreId { get; set; }

    public int SortOrder { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public virtual Store Store { get; set; } = null!;
}
