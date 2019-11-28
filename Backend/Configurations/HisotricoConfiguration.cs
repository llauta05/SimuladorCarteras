using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Configurations
{
    public class HisotricoConfiguration : IEntityTypeConfiguration<Historico>
    {
        public void Configure(EntityTypeBuilder<Historico> builder)
        {
            builder.ToTable("carteras_historico");
            builder.HasKey(c => c.Id);

            builder.Property(esp => esp.FechaOperacion).HasColumnName("fecha_operacion");
            builder.Property(esp => esp.TipoOperacion).HasColumnName("tipo_operacion");
            builder.Property(esp => esp.CarteraId).HasColumnName("id_cartera").IsRequired();

        }
    }
}
