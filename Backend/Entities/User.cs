using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Pass { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Foto { get; set; }
        public List<Cartera> Carteras { get; set; }
    }
}
