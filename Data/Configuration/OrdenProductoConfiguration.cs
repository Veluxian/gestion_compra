using Prueba_Tecnica.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Prueba_Tecnica.Data.Configuration
{
    public class OrdenProductoConfiguration : IEntityTypeConfiguration<OrdenProducto>
    {
        public void Configure(EntityTypeBuilder<OrdenProducto> builder)
        {
            builder.ToTable("ORDEN_PRODUCTO");

            builder.HasKey(e => e.IdOrdenProducto);

            builder.Property(e => e.IdOrdenProducto).HasColumnName("ORDEN_PRODUCTO");
            builder.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");
            builder.Property(e => e.IdOrden).HasColumnName("ID_ORDEN");

            builder.HasOne(e => e.OrdenCompra)
                   .WithMany(u => u.OrdenProductos)
                   .HasForeignKey(e => e.IdOrden);

            builder.HasOne(e => e.Producto)
                   .WithMany(u => u.OrdenProducto)
                   .HasForeignKey(e => e.IdProducto);
        }
    }
}