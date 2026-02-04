using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class Cart
{
    public long CartId { get; set; }

    public long AccountId { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
