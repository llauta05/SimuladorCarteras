using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
    public class Cartera
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activa { get; set; }
        public int UsuarioId { get; set; }
        public User Usuario { get; set; }
        public List<Especie> Especies { get; set; }
        public List<Historico> Historicos { get; set; }
    }   
}
