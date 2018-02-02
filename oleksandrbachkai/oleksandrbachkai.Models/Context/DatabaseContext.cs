using System.Data.Entity;
using oleksandrbachkai.Models.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace oleksandrbachkai.Models.Context
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static DatabaseContext Create()
        {
            return new DatabaseContext();
        }

        public DbSet<Page> Pages { get; set; }

        public DbSet<Folder> Folders { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}