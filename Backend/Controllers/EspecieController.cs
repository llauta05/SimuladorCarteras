using Backend.Data;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EspecieController : ControllerBase
    {
        private IEspecieService especieService;
        private ICarteraService carteraService;

        public EspecieController(IEspecieService especie, ICarteraService cartera)
        {
            carteraService = cartera;
            especieService = especie;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = especieService.GetEspeciesCotizaciones();
            return Ok(list);
        }
        [HttpPost("GetEspeciesDetalle")]
        public IActionResult GetEspeciesDetalle()
        {
            var list = especieService.GetEspeciesCotizacionesDetalle();
            return Ok(list);
        }
        [HttpPost("GetEspecieByTiket")]
        public IActionResult GetEspecieByTiket([FromBody]EspecieAPIData especie)
        {
            if (especie.TICKER == null || especie.TICKER == "")
                return BadRequest(new { message = "Debe especificar el TICKER" });

            return Ok(especieService.GetEspeciesCotizacionesByTicket(especie.TICKER.ToUpper()));
        }
        [HttpPost("GetEspecieByTiketHistory")]
        public IActionResult GetEspecieByTiketHistory([FromBody]EspecieAPIData especie)
        {
            if (especie.TICKER == null)
                return BadRequest(new { message = "Debe especificar el TICKER" });

            return Ok(especieService.GetEspeciesCotizacionesByTicketHistory(especie.TICKER.ToUpper()));
        }

        [HttpPost("GetEspecieByTiketDate")]
        public IActionResult GetEspecieByTiketDate([FromBody]EspecieAPIFechasData especie)
        {
            if (especie.Ticket == null || especie.Ticket == "")
                return BadRequest(new { message = "Debe especificar el TICKER" });

            return Ok(especieService.GetEspeciesCotizacionesByTicketDate(especie.Ticket.ToUpper(), especie.Desde.ToString("u", CultureInfo.CreateSpecificCulture("es-ES")), especie.Hasta.ToString("u", CultureInfo.CreateSpecificCulture("es-ES"))));
        }

        [HttpPost("GetEspecieMax")]
        public IActionResult GetEspecieMax([FromBody]EspecieAPIData especie)
        {
            if (especie.TICKER == null || especie.TICKER == "")
                return BadRequest(new { message = "Debe especificar el TICKER" });

            return Ok(especieService.GetEspecieMaximo(especie.TICKER));
        }

        [HttpPost("GetEspecieUltimoAño")]
        public IActionResult GetEspecieUltimoAño([FromBody]EspecieAPIData especie)
        {
            if (especie.TICKER == null || especie.TICKER == "")
                return BadRequest(new { message = "Debe especificar el TICKER" });

            return Ok(especieService.GetEspecieUltimoAño(especie.TICKER));
        }

        [HttpPut("Update")]
        public IActionResult UpdateEspecie([FromBody]EspecieData especieData)
        {
            var especie = especieService.GetEspecieById(especieData.Id);
            if(especie == null)
                return BadRequest(new { message = "Especie no existe" });
            if (especieData.TipoOperacion != "V" && especieData.TipoOperacion != "C")
                return BadRequest(new { message = "Tipo de operacion incorrecta" });
            if (especieData.TipoOperacion == "V" && especieData.Cantidad > especie.Cantidad)
                return BadRequest(new { message = "No se pueden quitar mas especies de las existentes." });

            return Ok(especieService.UpdateEspecie(especieData));
        }
        [HttpDelete("Delete")]
        public IActionResult Delete([FromBody]EspecieData especieData)
        {
            var especie = especieService.GetEspecieById(especieData.Id);
            if (especie == null)
                return BadRequest(new { message = "Especie no existe" });

            return Ok(especieService.DeleteEspecie(especieData.Id));
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody]EspecieAddData especieData)
        {
            var cartera = carteraService.GetCarteraById(especieData.CarteraId);
            if (cartera == null)
                return BadRequest(new { message = "La cartera no existe" });
            if (especieData.EspecieNombre == null)
                return BadRequest(new { message = "La especie debe tener un nombre" });
            if (especieData.Cantidad < 0)
                return BadRequest(new { message = "La cantidad tiene que ser mayor a 0." });

            return Ok(especieService.AddEspecie(especieData));
        }
        [HttpPost("GetEspecieMovimientoUltimoMes")]
        public IActionResult GetEspecieMovimientoUltimoMes([FromBody]EspecieAPIData especie)
        {
            if (especie.TICKER == null || especie.TICKER == "")
                return BadRequest(new { message = "Debe especificar el TICKER" });

            return Ok(especieService.GetMovimientoUltimoMes(especie.TICKER.ToUpper()));
        }
        [HttpPost("GetEspecieMediaMovil")]
        public IActionResult GetEspecieMediaMovil([FromBody]EspecieAPIData especie)
        {
            if (especie.TICKER == null || especie.TICKER == "")
                return BadRequest(new { message = "Debe especificar el TICKER" });

            return Ok(especieService.MediaMovil(especie.TICKER.ToUpper()));
        }
    }
}
