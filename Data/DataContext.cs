using EcoTrack.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EcoTrack.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Casa> Casa { get; set; }
        public DbSet<Agua> Agua { get; set; }
        public DbSet<Luz> Luz { get; set; }
        public DbSet<Residuo> Residuo { get; set; }
    }
}
