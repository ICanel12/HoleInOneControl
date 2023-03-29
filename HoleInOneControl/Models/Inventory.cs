using System;
using System.Collections.Generic;

namespace HoleInOneControl.Models;

public partial class Inventory
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ArticleId { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
