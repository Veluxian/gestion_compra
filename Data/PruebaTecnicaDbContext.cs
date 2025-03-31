using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Models;

namespace Prueba_Tecnica.Data
{
    public class PruebaTecnicaDbContext :DbContext
    {
        public PruebaTecnicaDbContext(DbContextOptions<PruebaTecnicaDbContext> options) :base(options) 
        {
        }

        public DbSet<OrdenCompra> ordenCompras { get; set; }
        public DbSet<Producto> productos { get; set; }
        public DbSet<OrdenProducto> ordenProductos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PruebaTecnicaDbContext).Assembly);
        }
    }
}