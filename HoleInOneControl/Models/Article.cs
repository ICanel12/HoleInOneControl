using System;
using System.Collections.Generic;

namespace HoleInOneControl.Models;

public partial class Article
{
    public int IdArticle { get; set; }

    public int? IdUser { get; set; }

    public string NameArticle { get; set; } = null!;

    public string? Brand { get; set; }

    public string? Model { get; set; }

    public int? Capacity { get; set; }

    public string? Color { get; set; }

    public string? Type { get; set; }

    public string? Material { get; set; }

    public string? Description { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<TransactionArticle> TransactionArticles { get; } = new List<TransactionArticle>();
}
