using System.Data.Entity;

namespace oleksandrbachkai.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=DefaultConnection")
        {

        }
        public DbSet<Page> Pages { get; set; }
    }
}
