using Blog.web.Models.Domain;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.web.Data
{
    public class BloggieDbContext : DbContext
    {
        public BloggieDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }
}
