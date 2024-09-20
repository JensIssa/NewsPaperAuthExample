using Microsoft.EntityFrameworkCore;
using NewsPaperAuthExample.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace NewsPaperAuthExample.Repo
{
    public class RepoContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public RepoContext(DbContextOptions<RepoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Editor", NormalizedName = "EDITOR" },
                new Role { Id = 2, Name = "Writer", NormalizedName = "WRITER" },
                new Role { Id = 3, Name = "Subscriber", NormalizedName = "SUBSCRIBER" },
                new Role { Id = 4, Name = "Guest", NormalizedName = "GUEST" }
            );
        }

    }
}
