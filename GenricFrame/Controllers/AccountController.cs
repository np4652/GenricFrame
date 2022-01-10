using GenricFrame.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GenricFrame.Controllers
{
    public class AccountController : Controller
    {
        private IConfiguration _config;
        private readonly AppSettings _appSettings;
        public AccountController(IConfiguration config, IOptions<AppSettings> appSettings)
        {
            _config = config;
            _appSettings = appSettings.Value;

        }
        private List<User> _users = new List<User>
         {
             new User { Id = 1, Username = "Amit", Password = "password" },
             new User { Id = 2, Username = "test", Password = "test" }
         };

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]LoginRequest model)
        {
            var response = Authenticate(model.UserName, model.Password);
            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(response);
        }

        // helper methods
        private AuthenticateResponse Authenticate(string userName,string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == userName && x.Password == password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }
        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            /* Claims */
            var claims = new[] {
                new Claim("id", user.Id.ToString()),
                new Claim("role", "Admin"),
                new Claim("userName", user.Username),
               // new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
               // new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
               // new Claim("DateOfJoing", userInfo.DateOfJoing.ToString("yyyy-MM-dd")),
               // new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            /* End Claims */
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}