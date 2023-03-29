using System;
using System.Collections.Generic;

namespace HoleInOneControl.Models;

public partial class TransactionArticle
{
    public int Id { get; set; }

    public int? IdArticle { get; set; }

    public string Type { get; set; } = null!;

    public virtual Article? IdArticleNavigation { get; set; }
}
