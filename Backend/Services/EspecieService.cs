using Backend.Context;
using Backend.Data;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IEspecieService
    {
        Tuple<DateTime, decimal> MediaMovil(string especie);
        EspeciesRendimiento GetMovimientoUltimoMes(string especie);
        List<EspecieFechaCierre> GetEspecieUltimoAño(string ticket);
        EspecieAPI GetEspecieMaximo(string ticket);
        List<EspecieAPI> GetEspeciesCotizacionesDetalle();
        List<EspecieAPI> GetEspeciesCotizacionesByTicketHistory(string ticket);
        List<EspecieAPI> GetEspeciesCotizacionesByTicketDate(string ticket, string desde, string hasta);
        EspecieAPI GetEspeciesCotizacionesByTicket(string ticket);
        List<string> GetEspeciesCotizaciones();
        bool AddEspecie(EspecieAddData especie);
        bool UpdateEspecie(EspecieData especie);
        bool DeleteEspecie(int especieId);
        Especie GetEspecieById(int especieId);
        bool AddEspecies(IEnumerable<Especie> especies);
        IEnumerable<Especie> GetEspecies();
    }
    public class EspecieService : IEspecieService
    {
        private readonly DbContextUser _context;
        private readonly ICotizacionesService cotizacionesService;
        private readonly IHistoricoService historicoService;
        public EspecieService(DbContextUser user, ICotizacionesService cotizaciones, IHistoricoService his)
        {
            historicoService = his;
            cotizacionesService = cotizaciones;
            _context = user;
        }

        public Especie GetEspecieById(int especieId)
        {
            return _context.Especies.Include(x => x.Cartera).Where(z => z.Id.Equals(especieId)).FirstOrDefault();
        }
        public bool DeleteEspecie(int especieId)
        {
            var especieDelete = GetEspecieById(especieId);
            _context.Especies.Remove(especieDelete);
            _context.SaveChanges();
            return true;

        }

        public bool UpdateEspecie(EspecieData especie)
        {
            var especieUpdate = GetEspecieById(especie.Id);

            especieUpdate.TipoOperacion = especie.TipoOperacion;
            especieUpdate.FechaOperacion = DateTime.Now;
            especieUpdate.Cantidad = (especie.TipoOperacion == "V") ? especieUpdate.Cantidad - especie.Cantidad : especieUpdate.Cantidad + especie.Cantidad;

            _context.Especies.Update(especieUpdate);
            _context.SaveChanges();

            var cierre = GetEspeciesCotizacionesByTicket(especieUpdate.EspecieNombre).CLOSE.GetValueOrDefault();
            return historicoService.SaveHistorico(especieUpdate.CarteraId, especieUpdate.TipoOperacion, especie.Cantidad, cierre);
        }

        public bool AddEspecie(EspecieAddData especie)
        {
            especie.TipoOperacion = "C"; //siempre el add es compra
            _context.Especies.Add(new Especie
            {
                EspecieNombre = especie.EspecieNombre,
                TipoOperacion = especie.TipoOperacion,
                FechaOperacion = DateTime.Now,
                CarteraId = especie.CarteraId,
                Cantidad = especie.Cantidad,
            });
            _context.SaveChanges();

            var cierre = GetEspeciesCotizacionesByTicket(especie.EspecieNombre).CLOSE.GetValueOrDefault();
            return historicoService.SaveHistorico(especie.CarteraId, especie.TipoOperacion, especie.Cantidad, cierre);
        }
        public IEnumerable<Especie> GetEspecies()
        {
            var algo = _context.Especies.Include(c => c.Cartera).ToList();

            return algo;

        }
        public bool AddEspecies(IEnumerable<Especie> especies)
        {

            _context.Especies.AddRange(especies);

            return true;

        }
        public List<string> GetEspeciesCotizaciones()
        {
            return cotizacionesService.GetEspecies().Result;
        }
        public List<EspecieAPI> GetEspeciesCotizacionesDetalle()
        {
            var lista = cotizacionesService.GetEspecies().Result;
            var otraCosa = lista.Select(x => GetEspeciesCotizacionesByTicket(x)).ToList();
            return otraCosa;
        }
        public EspecieAPI GetEspeciesCotizacionesByTicket(string ticket)
        {
            return cotizacionesService.GetEspecie(ticket).Result;
        }
        public List<EspecieAPI> GetEspeciesCotizacionesByTicketHistory(string ticket)
        {
            return cotizacionesService.GetEspecieHistorial(ticket).Result;
        }
        public List<EspecieAPI> GetEspeciesCotizacionesByTicketDate(string ticket, string desde, string hasta)
        {
            return cotizacionesService.GetEspecieFechas(ticket, desde.Substring(0, 10), hasta.Substring(0, 10)).Result;
        }
        public EspecieAPI GetEspecieMaximo(string ticket)
        {
            //FIXME: hardcodeo una fecha en el pasado porq si no no trae nunca nada.. Cuando este en prod poner: DateTime.Now;
            var hasta = Convert.ToDateTime("2019-08-16");
            var desde = hasta.AddDays(-30);
            var h = hasta.ToString("u", CultureInfo.CreateSpecificCulture("es-ES")).Substring(0, 10);
            var d = desde.ToString("u", CultureInfo.CreateSpecificCulture("es-ES")).Substring(0, 10);
            var especies = cotizacionesService.GetEspecieFechas(ticket, d, h).Result;
            var max = especies.Count() > 0 ? especies.Max(a => a.CLOSE.GetValueOrDefault()) : 0;
            return especies.Where(i => i.CLOSE.Equals(max)).FirstOrDefault();

        }
        public List<EspecieFechaCierre> GetEspecieUltimoAño(string ticket)
        {
            var hasta = DateTime.Now;
            var desde = hasta.AddYears(-1);
            var h = hasta.ToString("u", CultureInfo.CreateSpecificCulture("es-ES")).Substring(0, 10);
            var d = desde.ToString("u", CultureInfo.CreateSpecificCulture("es-ES")).Substring(0, 10);
            var especies = cotizacionesService.GetEspecieFechas(ticket, d, h).Result;
            return especies.Count() > 0 ? especies.Select(a => new EspecieFechaCierre
            {
                FechaCierre = a.DATE.ToString().Substring(0, 10),
                Cierre = a.CLOSE.GetValueOrDefault()
            }).ToList() : new List<EspecieFechaCierre> { };
        }
        public EspeciesRendimiento GetMovimientoUltimoMes(string especie)
        {
            DateTime hoy = DateTime.Now;
            var unMesAtras = hoy.AddDays(-150).ToString("u", CultureInfo.CreateSpecificCulture("es-ES"));
            var especies = GetEspeciesCotizacionesByTicketDate(especie, unMesAtras, hoy.ToString("u", CultureInfo.CreateSpecificCulture("es-ES")));
            decimal precioAyer = 0;
            decimal precioHoy = 0;
            int contadorSubas = 0;
            int contadorBajas = 0;
            int contadorSinCambios = 0;
            for (int i = 1; i < especies.Count; i++)
            {
                precioAyer = especies[i - 1].CLOSE.GetValueOrDefault();
                precioHoy = especies[i].CLOSE.GetValueOrDefault();
                var rendimiento = (precioHoy - precioAyer) / precioAyer;
                if (rendimiento > 0)
                    contadorSubas++;
                if (rendimiento < 0)
                    contadorBajas++;
                if (rendimiento == 0)
                    contadorSinCambios++;
            }
            return new EspeciesRendimiento
            {
                Alzas = contadorSubas,
                Bajas = contadorBajas,
                SinCambios = contadorSinCambios
            };
        }

        public Tuple<DateTime, decimal> MediaMovil(string especie)
        {
            var historial = GetEspeciesCotizacionesByTicketHistory(especie);
            var media = calcularMediaMovil(historial, 5);
            Tuple<DateTime, decimal> ultimovalorMediaMovil = media[media.Count - 1];

            return ultimovalorMediaMovil;
        }

        static List<Tuple<DateTime, decimal>> calcularMediaMovil(List<EspecieAPI> listaDeCotizaciones, int frecuenciaDeLaMedia)
        {
            List<Tuple<DateTime, decimal>> mediaMovil = new List<Tuple<DateTime, decimal>>();
            int contador = 0;
            decimal sumador = 0;

            for (int i = 0; i < listaDeCotizaciones.Count; i++)
            {
                sumador += listaDeCotizaciones[i].CLOSE.GetValueOrDefault();
                contador++;
                if (contador == frecuenciaDeLaMedia)
                {
                    var resultadoPromedioMovil = sumador / contador;
                    var fechaResultadoPromedioMovil = listaDeCotizaciones[i].DATE.GetValueOrDefault();
                    mediaMovil.Add(new Tuple<DateTime, decimal>(fechaResultadoPromedioMovil, resultadoPromedioMovil));
                    sumador = 0;
                    contador = 0;
                }
            }
            return mediaMovil;
        }
    }
}
