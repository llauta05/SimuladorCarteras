using Backend.Entities;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class HistoricoController : ControllerBase
    {
        private IHistoricoService historicoService;
        
        public HistoricoController(IHistoricoService his)
        {
            historicoService = his;
        }

        [HttpPost("GetHistoricoByCarteraId")]
        public IActionResult GetHisotricoByCarteraId([FromBody]Historico historico)
        {
            return Ok(historicoService.GetHistoricos(historico.CarteraId));
        }
        [HttpPost("GetHistoricoValorByCarteraId")]
        public IActionResult GetHisotricoValorByCarteraId([FromBody]Historico historico)
        {
            return Ok(historicoService.GetHistoricosCartera(historico.CarteraId));
        }
    }
}
