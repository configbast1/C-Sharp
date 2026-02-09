using Microsoft.EntityFrameworkCore;
using testing.Models;

namespace testing
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!; 

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=TestingDb;Trusted_Connection=True;"
            );
        }
    }
}
