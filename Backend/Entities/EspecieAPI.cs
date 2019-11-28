using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
    public class EspecieAPI
    {
        public int ID { get; set; }
        public string TICKER { get; set; }
        public string PER { get; set; }
        public DateTime? DATE { get; set; }
        public string TIME { get; set; }
        public decimal? OPEN { get; set; }
        public decimal? HIGH { get; set; }
        public decimal? LOW { get; set; }
        public decimal? CLOSE { get; set; }
        public decimal? VOLUME { get; set; }
    }
}
