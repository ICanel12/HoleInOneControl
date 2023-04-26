using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoleInOneControlModel
{
    public class Article
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

        public List<SelectListItem>? Users { get; set; }
    }
}
