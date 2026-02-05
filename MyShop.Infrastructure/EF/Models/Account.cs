using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class Account
{
    public long AccountId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Email { get; set; }

    public int Status { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public virtual ICollection<AccountRole> AccountRoles { get; set; } = new List<AccountRole>();

    public virtual AdminAccount? AdminAccount { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<FavoriteStore> FavoriteStores { get; set; } = new List<FavoriteStore>();

    public virtual MemberProfile? MemberProfile { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual VendorProfile? VendorProfile { get; set; }
}
