using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class Category
{
    public long CategoryId { get; set; }

    public long? ParentCategoryId { get; set; }

    public long? StoreId { get; set; }

    public string CategoryName { get; set; } = null!;

    public int Level { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();

    public virtual Category? ParentCategory { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual Store? Store { get; set; }
}
