using Microsoft.EntityFrameworkCore;
using Refrigrator.Models;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace Refrigrator.Data
{
    public class AppDbContext: DbContext
    {

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db"); // SQLite database file name
        }
    }
}
