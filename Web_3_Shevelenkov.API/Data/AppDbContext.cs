using Microsoft.EntityFrameworkCore;
using Web_3_Shevelenkov.Domain.Entities;

namespace Web_3_Shevelenkov.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Tank> Tanks { get; set; }
        public DbSet<TankType> TankTypes { get; set; }
    }
}
