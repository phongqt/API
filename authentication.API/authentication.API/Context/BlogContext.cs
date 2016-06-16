using authentication.API.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace authentication.API.Context
{
    public class BlogContext : System.Data.Entity.DbContext
    {
        public BlogContext(): base("BlogContext")
        {
            System.Data.Entity.Database.SetInitializer<BlogContext>(null);
        }
        public System.Data.Entity.DbSet<User> Users { get; set; }
        public System.Data.Entity.DbSet<Article> Articles { get; set; }
    }
}