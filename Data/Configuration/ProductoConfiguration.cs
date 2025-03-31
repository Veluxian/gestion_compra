using Prueba_Tecnica.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Prueba_Tecnica.Data.Configuration
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("PRODUCTO");

            builder.HasKey(e => e.IdProducto);

            builder.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");
            builder.Property(e => e.Nombre).HasColumnName("NOMBRE_PRODUCTO");
            builder.Property(e => e.Precio).HasColumnName("PRECIO_PRODUCTO");

        }
    }
}