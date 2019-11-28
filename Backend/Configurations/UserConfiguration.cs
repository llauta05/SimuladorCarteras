using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("usuarios");
            builder.HasKey(c => c.Id);

            builder.Property(user => user.NombreUsuario).HasColumnName("nombre_usuario");
            builder.Property(user => user.FechaAlta).HasColumnName("fecha_alta");
        }
    }

}
