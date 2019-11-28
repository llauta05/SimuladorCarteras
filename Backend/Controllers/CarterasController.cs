using Backend.Data;
using Backend.Entities;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CarterasController : ControllerBase
    {
        private ICarteraService carteraService;
        private IUserService _userService;

        public CarterasController(ICarteraService cartera, IUserService userService)
        {
            _userService = userService;
            carteraService = cartera;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(carteraService.GetCarteras());
        }
       
        [HttpPost("GetCarteraUsuario")]
        public IActionResult GetCarterasUser([FromBody]User user)
        {
            if (user.NombreUsuario == null)
                return BadRequest(new { message = "No se envio nombre de usuario" });

            return Ok(carteraService.GetCarterasUsuario(user.NombreUsuario));
        }

        [HttpPost("GetCarteraById")]
        public IActionResult GetCarteraById([FromBody]CarteraUserData carteraUser)
        {
            var cartera = carteraService.GetCarteraById(carteraUser.Cartera.Id);
            if (cartera == null)
                return BadRequest(new { message = "Cartera invalida" });

            return Ok(cartera);
        }


        [HttpPost("AddCarteraUser")]
        public IActionResult AddCarteraUser([FromBody]CarteraUserData carteraUser)
        {
            var user = _userService.GetUserByName(carteraUser.NombreUser);
            if (user == null)
                return BadRequest(new { message = "Nombre de usuario invalido" });

            if (carteraUser.Cartera.Nombre == null)
                return BadRequest(new { message = "Informacion insuficiente" });

            if (carteraService.ExistCartera(carteraUser))
                return BadRequest(new { message = "La Cartera ya existe" });

            return Ok(carteraService.AddCartera(carteraUser.Cartera, user.Id));
        }
            
        [HttpPost("DisableCartera")]
        public IActionResult DisableCartera([FromBody]CarteraUserData carteraUser) {
            var user = _userService.GetUserByName(carteraUser.NombreUser);
            if (user == null)
                return BadRequest(new { message = "Nombre de usuario invalido" });

            var cartera = carteraService.GetCarteraById(carteraUser.Cartera.Id);
            if(cartera == null)
                return BadRequest(new { message = "Cartera invalida" });

            return Ok(carteraService.DisableCartera(cartera));

        }
    }
}
