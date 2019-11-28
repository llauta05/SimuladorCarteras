using Backend.Data;
using Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Mappers
{
    public static class EspecieMapping
    {
        public static EspecieData MapToEspecie(this Especie especie)
        {
            return new EspecieData
            {
                Id = especie.Id,
                Cantidad = especie.Cantidad,
                EspecieNombre= especie.EspecieNombre,
                FechaOperacion= especie.FechaOperacion,
                TipoOperacion = especie.TipoOperacion
            };
        }

    }
}
