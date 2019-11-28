using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Configurations
{
    public class CarteraConfiguration : IEntityTypeConfiguration<Cartera>
    {
        public void Configure(EntityTypeBuilder<Cartera> builder)
        {
            builder.ToTable("carteras");
            builder.HasKey(c => c.Id);


            builder.Property(user => user.FechaCreacion).HasColumnName("fecha_creacion");
            builder.Property(user => user.UsuarioId).HasColumnName("idUsuario");



        }
    }
}
