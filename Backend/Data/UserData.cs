using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Backend.Data
{
    [DataContract]
    public class UserTokenData
    {
        [DataMember(Name = "nombreUsuario")]
        public string UserName { get; set; }
        [DataMember(Name = "token")]
        public string Token { get; set; }
    }
    [DataContract]
    public class UserData
    {
        [DataMember(Name = "nombre")]
        public string Nombre { get; set; }
        [DataMember(Name = "apellido")]
        public string Apellido { get; set; }
        [DataMember(Name = "nombreUsuario")]
        public string NombreUsuario { get; set; }
        [DataMember(Name ="fechaAlta")]
        public DateTime FechaAlta { get; set; }
    }
    public class UserCarteraData
    {
        [DataMember(Name = "nombreUsuario")]
        public string NombreUsuario { get; set; }
        [DataMember(Name = "Carteras")]
        public IEnumerable<CarteraData> Carteras { get; set; }
    }
}
