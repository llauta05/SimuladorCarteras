using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Backend.Data
{
    [DataContract]
    public class EspecieAPIData
    {
        [DataMember(Name ="id")]
        public int ID { get; set; }
        [DataMember(Name ="ticker")]
        public string TICKER { get; set; }
        [DataMember(Name="per")]
        public string PER { get; set; }
        [DataMember(Name ="date")]
        public DateTime? DATE { get; set; }
        [DataMember(Name ="time")]
        public string TIME { get; set; }
        [DataMember(Name="open")]
        public decimal? OPEN { get; set; }
        [DataMember(Name="high")]
        public decimal? HIGH { get; set; }
        [DataMember(Name = "low")]
        public decimal? LOW { get; set; }
        [DataMember(Name = "close")]
        public decimal? CLOSE { get; set; }
        [DataMember(Name = "volume")]
        public decimal? VOLUME { get; set; }
    }
    [DataContract]
    public class EspecieAPIFechasData
    {
        [DataMember(Name ="ticket")]
        public string Ticket { get; set; }
        [DataMember(Name ="fechaDesde")]
        public DateTime Desde { get; set; }
        [DataMember(Name ="fechaHasta")]
        public DateTime Hasta { get; set; }
    }
}
