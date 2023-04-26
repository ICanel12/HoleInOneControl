using System;
using System.Collections.Generic;

namespace HoleInOneControlAPI.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string UserName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<Handicap> Handicaps { get; set; } = new List<Handicap>();
}
