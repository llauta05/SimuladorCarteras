using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Configurations
{
    public class EspecieConfiguration : IEntityTypeConfiguration<Especie>
    {
        public void Configure(EntityTypeBuilder<Especie> builder)
        {
            builder.ToTable("carteras_composicion");
            builder.HasKey(c => c.Id);


            builder.Property(esp => esp.FechaOperacion).HasColumnName("fecha_operacion");
            builder.Property(esp => esp.EspecieNombre).HasColumnName("especie");
            builder.Property(esp => esp.TipoOperacion).HasColumnName("tipo_operacion");
            builder.Property(esp => esp.CarteraId).HasColumnName("id_cartera").IsRequired();

        }
    }
}
