using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
    public class Historico
    {
        public int Id { get; set; }
        public DateTime FechaOperacion { get; set; }
        public string TipoOperacion { get; set; }
        public int Cantidad { get; set; }
        public decimal Valor { get; set; }
        public int CarteraId { get; set; }
        public Cartera Cartera { get; set; }
    }
}
