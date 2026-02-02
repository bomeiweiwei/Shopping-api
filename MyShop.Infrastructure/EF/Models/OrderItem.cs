using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class OrderItem
{
    public long OrderItemId { get; set; }

    public long OrderId { get; set; }

    public long ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
