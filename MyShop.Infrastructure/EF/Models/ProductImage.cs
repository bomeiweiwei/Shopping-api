using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class ProductImage
{
    public long ImageId { get; set; }

    public long ProductId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public int SortOrder { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Product Product { get; set; } = null!;
}
