using Backend.Entities;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Backend.Data
{
    [DataContract]
    public class CarteraData
    {
        [DataMember(Name ="id")]
        public int Id { get; set; }
        [DataMember(Name ="nombre")]
        public string Nombre { get; set; }
        [DataMember(Name ="descripcion")]
        public string Descripcion { get; set; }
        [DataMember(Name ="cantidadEspecies")]
        public int CantidadEspecies { get; set; }
        [DataMember(Name = "activa")]
        public bool Activa { get; set; }
        [DataMember(Name ="especies")]
        public IEnumerable<EspecieCotizaData> Especies { get; set; }

    }
    [DataContract]
    public class CarteraUserData
    {
        [DataMember(Name = "userNombre")]
        public string NombreUser { get; set; }
        [DataMember(Name ="Cartera")]
        public Cartera Cartera { get; set; }
    }
}
