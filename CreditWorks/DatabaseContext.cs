using CreditWorks.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditWorks
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<WeightCategories> WeightCategories { get; set; }
    }
}
