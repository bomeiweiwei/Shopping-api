using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class Order
{
    public long OrderId { get; set; }

    public long BuyerAccountId { get; set; }

    public long StoreId { get; set; }

    public int OrderStatus { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public virtual Account BuyerAccount { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Store Store { get; set; } = null!;
}
