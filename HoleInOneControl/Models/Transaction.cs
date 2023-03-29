using System;
using System.Collections.Generic;

namespace HoleInOneControl.Models;

public partial class Transaction
{
    public int IdTransaction { get; set; }

    public int? IdUser { get; set; }

    public DateTime? DateHour { get; set; }

    public string Type { get; set; } = null!;

    public virtual User? IdUserNavigation { get; set; }
}
