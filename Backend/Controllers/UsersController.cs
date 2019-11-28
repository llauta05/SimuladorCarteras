using Backend.Entities;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.NombreUsuario, userParam.Pass);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users =  _userService.GetAll();
            return Ok(users);
        }
        [AllowAnonymous]
        [HttpPost("AddUser")]
        public IActionResult AddUser([FromBody]User userParam)
        {
            if(_userService.GetUserExiste(userParam))
                return BadRequest(new { message = "El nombre de usuario fue utilizado" });

            return Ok(_userService.AddUser(userParam));
        }
        [HttpPost("GetUserById")]
        public IActionResult GetUserById([FromBody]User userParam)
        {
            return Ok(_userService.GetUserByNameMap(userParam.NombreUsuario));
        }
    }
}
