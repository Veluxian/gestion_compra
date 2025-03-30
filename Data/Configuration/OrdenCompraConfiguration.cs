using Prueba_Tecnica.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Prueba_Tecnica.Data.Configuration
{
    public class OrdenCompraConfiguration :IEntityTypeConfiguration<OrdenCompra>
    {
        public void Configure(EntityTypeBuilder<OrdenCompra> builder)
        {
            builder.ToTable("ORDEN_DE_COMPRA");

            builder.HasKey(e => e.IdOrden);

            builder.Property(e => e.IdOrden).HasColumnName("ID_ORDEN");
            builder.Property(e => e.Cliente).HasColumnName("CLIENTE");
            builder.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");
            builder.Property(e => e.Total).HasColumnName("TOTAL");
        }
    }
}