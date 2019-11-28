using Backend.Context;
using Backend.Data;
using Backend.Entities;
using Backend.Helpers;
using Backend.Mappers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Backend.Services
{
    public interface IUserService
    {
        UserData GetUserByNameMap(string nickName);
        User GetUserByName(string nickName);
        UserData AddUser(User user);
        UserTokenData Authenticate(string username, string password);
        IEnumerable<UserData> GetAll();
        bool GetUserExiste(User user);
    }
    public class UserService : IUserService
    {
        private readonly DbContextUser _context;

        public UserService(DbContextUser user, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _context = user;
        }
        private readonly AppSettings _appSettings;

        public UserTokenData Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.NombreUsuario.Trim().Equals(username.Trim()) && x.Pass.Trim().Equals(password.Trim()));

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.NombreUsuario)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userToken = new UserTokenData() { UserName = username, Token = tokenHandler.WriteToken(token) };

            return userToken;
        }

        public IEnumerable<UserData> GetAll()
        {
            return _context.Users.Select(x => x.MapToUser());
        }
        public UserData AddUser(User user)
        {
            user.FechaAlta = DateTime.Now;
            _context.Users.AddAsync(user);
            _context.SaveChanges();
            return user.MapToUser();
        }
        public bool GetUserExiste(User user)
        {
            var usuarioExiste = _context.Users.Where(u => u.NombreUsuario.Equals(user.NombreUsuario)).FirstOrDefault();
            if (usuarioExiste == null)
                return false;
            return true;
        }
        public User GetUserByName(string nickName)
        {
            if (nickName == null)
                throw new Exception("se debe especificar el nombre del usuario");
            return _context.Users.Where(x => x.NombreUsuario.Equals(nickName.Trim(), StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }
        public UserData GetUserByNameMap(string nickName)
        {
            return GetUserByName(nickName).MapToUser();
        }

    }
}
