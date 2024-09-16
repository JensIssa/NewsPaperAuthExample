using Microsoft.EntityFrameworkCore;
using NewsPaperAuthExample.Entities;

namespace NewsPaperAuthExample.Repo
{
    public class RepoContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }

        public RepoContext(DbContextOptions<RepoContext> options) : base(options)
        {
        }
    }
}
