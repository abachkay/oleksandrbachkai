using System.Data.Entity;
using oleksandrbachkai.Models.Entities;

namespace oleksandrbachkai.Models.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=DefaultConnection")
        {

        }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<File> Files { get; set; }
    }
}