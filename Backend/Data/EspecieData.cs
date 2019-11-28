using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Backend.Data
{
    [DataContract]
    public class EspecieData
    {
        [DataMember(Name ="id")]
        public int Id { get; set; }
        [DataMember(Name ="especieNombre")]
        public string EspecieNombre { get; set; }
        [DataMember(Name ="fechaOperacion")]
        public DateTime FechaOperacion { get; set; }
        [DataMember(Name ="tipoOperacion")]
        public string TipoOperacion { get; set; }
        [DataMember(Name = "cantidad")]
        public int Cantidad { get; set; }
    }
    public class EspecieCotizaData
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "especie")]
        public EspecieAPIData Especie { get; set; }
        [DataMember(Name = "fechaOperacion")]
        public DateTime FechaOperacion { get; set; }
        [DataMember(Name = "tipoOperacion")]
        public string TipoOperacion { get; set; }
        [DataMember(Name = "cantidad")]
        public int Cantidad { get; set; }
        [DataMember(Name ="porcentajeDia")]
        public decimal PorcentajeDia { get; set; }
        [DataMember(Name = "porcentajeTotal")]
        public decimal PorcentajeTotal { get; set; }
    }
    [DataContract]
    public class EspecieAddData
    {
        [DataMember(Name ="carteraId")]
        public int CarteraId { get; set; }
        [DataMember(Name = "especieNombre")]
        public string EspecieNombre { get; set; }
        [DataMember(Name = "tipoOperacion")]
        public string TipoOperacion { get; set; }
        [DataMember(Name = "cantidad")]
        public int Cantidad { get; set; }
    }
    [DataContract]
    public class EspecieFechaCierre
    {
        [DataMember(Name ="fecha")]
        public string FechaCierre { get; set; }
        [DataMember(Name = "cierre")]
        public decimal Cierre { get; set; }
    }
    [DataContract]
    public class EspeciesRendimiento
    {
        [DataMember(Name ="alzas")]
        public int Alzas { get; set; }
        [DataMember(Name = "bajas")]
        public int Bajas { get; set; }
        [DataMember(Name = "sinCambios")]
        public int SinCambios { get; set; }
    }
}
