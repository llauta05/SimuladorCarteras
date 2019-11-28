using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
    public class Especie
    {
        public int Id { get; set; }
        public string EspecieNombre { get; set; }
        public DateTime FechaOperacion { get; set; }
        public string TipoOperacion { get; set; }
        public int Cantidad { get; set; }
        public int CarteraId { get; set; }
        public Cartera Cartera { get; set; }
    }
}
