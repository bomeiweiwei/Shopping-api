using System;
using System.Collections.Generic;

namespace MyShop.Infrastructure.EF.Models;

public partial class SystemAnnouncement
{
    public long AnnouncementId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime? PublishAt { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
}
