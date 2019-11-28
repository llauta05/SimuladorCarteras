using Backend.Context;
using Backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface ICotizacionesService
    {
        Task<List<EspecieAPI>> GetEspecieHistorial(string especie);
        Task<List<EspecieAPI>> GetEspecieFechas(string especie, string desde, string hasta);
        Task<EspecieAPI> GetEspecie(string especie);
        Task<List<string>> GetEspecies();
    }
        public class CotizacionesService : ICotizacionesService
    {
        private readonly DbContextUser _context;

        public CotizacionesService(DbContextUser user)
        {
            _context = user;
        }
        public async Task<List<string>> GetEspecies()
        {
            List<string> reservationList = new List<string>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:52005/api/cotizaciones/listarespecies"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<string>>(apiResponse);
                }
            }
            return reservationList;
        }
      
        public async Task<List<EspecieAPI>> GetEspecieFechas(string especie,string desde, string hasta)
        {
            List<EspecieAPI> reservationList = new List<EspecieAPI>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:52005/api/cotizaciones/listar?especie="+especie+"&fechaDesde="+desde+"&fechaHasta="+hasta))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<EspecieAPI>>(apiResponse);
                }
            }
            return reservationList;
        }
        public async Task<EspecieAPI> GetEspecie(string especie)
        {
            EspecieAPI reservationList = new EspecieAPI();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:52005/api/cotizaciones/ultimocierre?especie="+especie))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<EspecieAPI>(apiResponse);
                }
            }
            return reservationList;
        }

        public async Task<List<EspecieAPI>> GetEspecieHistorial(string especie)
        {
            List<EspecieAPI> reservationList = new List<EspecieAPI>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:52005/api/cotizaciones/listar?especie=" + especie))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<EspecieAPI>>(apiResponse);
                }
            }
            return reservationList;
        }
    }
}
