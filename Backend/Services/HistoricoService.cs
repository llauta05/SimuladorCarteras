using Backend.Context;
using Backend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Mappers;
using Microsoft.EntityFrameworkCore;
using Backend.Entities;
using System.Globalization;

namespace Backend.Services
{
    public interface IHistoricoService
    {
        List<HistoricoCierresData> GetHistoricosCartera(int carteraId);
        List<HistoricoData> GetHistoricos(int carteraId);
        bool SaveHistorico(int carteraId, string ope, int cant, decimal valor);
    }
    public class HistoricoService : IHistoricoService
    {
        private readonly DbContextUser _context;
        private readonly IUserService _userService;
        private readonly ICotizacionesService _cotizacionesService;
        private readonly ICarteraService _carteraService;

        public HistoricoService(DbContextUser user, IUserService userService, ICotizacionesService cotizaciones, ICarteraService cartera)
        {
            _carteraService = cartera;
            _cotizacionesService = cotizaciones;
            _userService = userService;
            _context = user;
        }

        public List<HistoricoData> GetHistoricos(int carteraId)
        {
            var historicos = _context.Historicos.Where(x => x.CarteraId == carteraId).GroupBy(x => x.FechaOperacion.Date).Select(x => x.Key).ToList();

            return historicos.Select(x => GetValoresHistoricosHasta(carteraId, x)).ToList();

        }
        public List<HistoricoCierresData> GetHistoricosCartera(int carteraId)
        {
            var historicos = _context.Historicos.Where(x => x.CarteraId == carteraId).GroupBy(x => x.FechaOperacion.Date).Select(x => x.Key).ToList();

            return historicos.Select(x => GetCarteraHistoricosHasta(carteraId, x)).ToList();

        }

        private HistoricoCierresData GetCarteraHistoricosHasta(int carteraId, DateTime fecha)
        {
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            //var d = fecha.AddYears(-1).ToString("u", culture).Substring(0, 10);
            var h = fecha.ToString("u", culture).Substring(0, 10);
            //var especies = _carteraService.GetCarteraById(carteraId).Especies.ToList();
            //var cierres = especies.Select(x => _cotizacionesService.GetEspecieFechas(x.Especie.TICKER, d, h).Result.Last().CLOSE * x.Cantidad).ToList();
             var operaciones = _context.Historicos.Where(x => x.CarteraId == carteraId && x.FechaOperacion.Date <= fecha.Date).ToList();

            return new HistoricoCierresData
            {
                Fecha = h,
                Valor = operaciones.Sum(x => x.Valor)
            };
        }


        private HistoricoData GetValoresHistoricosHasta(int carteraId, DateTime fecha)
        {
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            var operaciones = _context.Historicos.Where(x => x.CarteraId == carteraId && x.FechaOperacion.Date <= fecha.Date).ToList();
            return new HistoricoData
            {
                Fecha = fecha.ToString("u", culture).Substring(0, 10),
                CantidadEspecies = operaciones.Sum(s => s.Cantidad)
            };
        }
        public bool SaveHistorico(int carteraId, string ope, int cant, decimal valor)
        {
            if (ope == "V")
            {
                cant *= -1;
                valor = (cant * valor) * - 1;
            }
            else
            {
                valor = cant * valor;
            }

            var historico = new Historico { CarteraId = carteraId, FechaOperacion = DateTime.Now, TipoOperacion = ope, Cantidad = cant, Valor = valor};

            _context.Historicos.Add(historico);
            _context.SaveChanges();

            return true;
        }
    }
}
