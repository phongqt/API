using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace authentication.API.Models
{
    [Table("Article")]
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string _Content { get; set; }
        public Nullable<DateTime> Created { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Image { get; set; }
        public string Alias { get; set; }
    }
}
