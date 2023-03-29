using System;
using System.Collections.Generic;

namespace HoleInOneControl.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string UserName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Article> Articles { get; } = new List<Article>();

    public virtual ICollection<Handicap> Handicaps { get; } = new List<Handicap>();

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
