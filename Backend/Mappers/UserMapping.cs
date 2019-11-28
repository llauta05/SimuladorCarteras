using Backend.Data;
using Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Mappers
{
    public static class UserMapping
    {
        public static UserData MapToUser(this User user)
        {
            return new UserData
            {
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                NombreUsuario = user.NombreUsuario,
                FechaAlta = user.FechaAlta
            };
        }
    }
}
