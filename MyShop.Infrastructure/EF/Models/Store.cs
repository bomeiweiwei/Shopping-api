using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class Store
{
    public long StoreId { get; set; }

    public long VendorId { get; set; }

    public string StoreName { get; set; } = null!;

    public string? Description { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<FavoriteStore> FavoriteStores { get; set; } = new List<FavoriteStore>();

    public virtual HotStore? HotStore { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<StoreAdvertisement> StoreAdvertisements { get; set; } = new List<StoreAdvertisement>();

    public virtual VendorProfile Vendor { get; set; } = null!;
}
