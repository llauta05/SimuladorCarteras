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
    public interface ICarteraService
    {
        bool ExistCartera(CarteraUserData cartera);
        bool DisableCartera(CarteraData cartera);
        CarteraData GetCarteraById(int carteraId);
        IEnumerable<CarteraData> GetCarteras();
        IEnumerable<CarteraData> GetCarterasUsuario(string userName);
        CarteraData AddCartera(Cartera cartera, int userId);

    }
    public class CarteraService : ICarteraService
    {
        private readonly DbContextUser _context;
        private readonly IUserService _userService;
        private readonly ICotizacionesService _cotizacionesService;

        public CarteraService(DbContextUser user, IUserService userService, ICotizacionesService cotizaciones)
        {
            _cotizacionesService = cotizaciones;
            _userService = userService;
            _context = user;
        }

        public CarteraData AddCartera(Cartera cartera, int userId)
        {
            cartera.UsuarioId = userId;
            cartera.FechaCreacion = DateTime.Now;
            cartera.Activa = true;
            cartera.Especies = new List<Especie>();
            _context.Carteras.Add(cartera);
            _context.SaveChanges();
            return cartera.MapToCartera();
        }
        public CarteraData GetCarteraById(int carteraId)
        {
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            var x = _context.Carteras.Include(e => e.Especies).Where(i => i.Id.Equals(carteraId)).FirstOrDefault();
            return new CarteraData
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                Nombre = x.Nombre,
                CantidadEspecies = x.Especies.Select(cant => cant.Cantidad).Sum(sum => sum),
                Especies = x.Especies.Select(e =>
                    new EspecieCotizaData
                    {
                        Id = e.Id,
                        Especie = _cotizacionesService.GetEspecie(e.EspecieNombre).Result.MapToEspecieAPI(),
                        FechaOperacion = e.FechaOperacion,
                        Cantidad = e.Cantidad,
                        TipoOperacion = e.TipoOperacion,
                        PorcentajeDia = PorcentajeVariacion(e, DateTime.Now.AddMonths(-8).ToString("u", culture).Substring(0, 10), DateTime.Now.ToString("u", culture).Substring(0, 10)),
                        PorcentajeTotal = PorcentajeVariacion(e, e.FechaOperacion.ToString("u", culture).Substring(0, 10), DateTime.Now.ToString("u", culture).Substring(0, 10))
                    }).ToList(),
            }; 
        }

        private decimal PorcentajeVariacion(Especie especie, string desde, string hasta)
        {
            var listado = _cotizacionesService.GetEspecieFechas(especie.EspecieNombre, desde,hasta).Result;
            if (listado.Count == 0)
                return 0;
            var nuevo = listado.Last();
            var anterior = listado.FirstOrDefault(x => x.ID == nuevo.ID - 1);

            var paso1 = nuevo.CLOSE / anterior.CLOSE;
            var paso2 = paso1 * 100;
            var paso3 = decimal.Round(paso2.GetValueOrDefault() - 100, 2, MidpointRounding.AwayFromZero);

            return paso3;
        }
        public bool ExistCartera(CarteraUserData input)
        {
            return CarterasUser(input.NombreUser).Where(x => x.Nombre.Equals(input.Cartera.Nombre.Trim())).FirstOrDefault() != null;
        }

        public bool DisableCartera(CarteraData cartera)
        {
            var cart = _context.Carteras.FirstOrDefault(x => x.Id == cartera.Id);
            cart.Activa = false;
            _context.Carteras.Update(cart);
            _context.SaveChanges();
            return true;
        }
        public IEnumerable<CarteraData> GetCarteras()
        {
            var carteras = _context.Carteras.Include(e => e.Especies);

            return carteras.Select(x => x.MapToCartera());
        }

        public IEnumerable<CarteraData> GetCarterasUsuario(string userName)
        {
            var culture = CultureInfo.CreateSpecificCulture("es-ES");
            //            var carteras = CarterasUser(userName).Where(x => x.Activa);
            var carteras = CarterasUser(userName);
            var carterasApi = carteras.Select(x =>
                new CarteraData
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    Activa = x.Activa,
                    Nombre = x.Nombre,
                    CantidadEspecies = x.Especies.Select(cant => cant.Cantidad).Sum(sum => sum),
                    Especies = x.Especies.Select(e =>
                        new EspecieCotizaData
                        {
                            Id = e.Id,
                            Especie = _cotizacionesService.GetEspecie(e.EspecieNombre).Result.MapToEspecieAPI(),
                            FechaOperacion = e.FechaOperacion,
                            Cantidad = e.Cantidad,
                            TipoOperacion = e.TipoOperacion,
                            PorcentajeDia = PorcentajeVariacion(e, DateTime.Now.AddMonths(-8).ToString("u", culture).Substring(0, 10), DateTime.Now.ToString("u", culture).Substring(0, 10)),
                            PorcentajeTotal = PorcentajeVariacion(e, e.FechaOperacion.ToString("u", culture).Substring(0, 10), DateTime.Now.ToString("u", culture).Substring(0, 10))
                        }).ToList(),
                });;
            var result = carterasApi.Select(x => x.CantidadEspecies = x.Especies.Count());
             
            return carterasApi;
        }

        private IEnumerable<Cartera> CarterasUser(string userName)
        {
            return _context.Carteras.Include(e => e.Especies).Where(x => x.Usuario.NombreUsuario.Equals(userName));
        }

    }
}
