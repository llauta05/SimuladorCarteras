using Backend.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Backend.Data
{
    [DataContract]
    public class HistoricoData
    {
        [DataMember(Name ="fecha")]
        public string Fecha { get; set; }
        [DataMember(Name ="cantidadEspecies")]
        public int CantidadEspecies { get; set; }
    }
    public class HistoricoCierresData
    {
        [DataMember(Name = "fecha")]
        public string Fecha { get; set; }
        [DataMember(Name = "valor")]
        public decimal Valor { get; set; }
    }

}
