using Backend.Data;
using Backend.Entities;
using Backend.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Mappers
{
    public static class EspecieAPIMapping
    {
        public static EspecieAPIData MapToEspecieAPI(this EspecieAPI especie)
        {
            return new EspecieAPIData
            {
                ID = especie.ID,
                CLOSE = especie.CLOSE,
                DATE = especie.DATE,
                HIGH = especie.HIGH,
                LOW = especie.LOW,
                OPEN = especie.OPEN,
                PER = especie.PER,
                TICKER = especie.TICKER,
                TIME = especie.TIME,
                VOLUME = especie.VOLUME
            };
        }
    }
}
