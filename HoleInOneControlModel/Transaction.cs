using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoleInOneControlModel
{
    public class Transaction
    {
        public int IdTransaction { get; set; }

        public int? IdUser { get; set; }

        public DateTime? DateHour { get; set; }

        public string? TypeTransaction { get; set; }

        public string NameArticle { get; set; } = null!;

        public string? Brand { get; set; }

        public string? Model { get; set; }

        public int? Capacity { get; set; }

        public string? Color { get; set; }

        public string? Type { get; set; }

        public string? Material { get; set; }

        public string? Description { get; set; }

        public string? UserName { get; set; }
    }
}
