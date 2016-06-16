using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace authentication.API.Models
{
    public class UserExt
    {
        public List<User> users { get; set; }
        public int totalRecords { get; set; }
    }
    public class ArticleExt
    {
        public List<Article> articles { get; set; }
        public int totalRecords { get; set; }
    }
}