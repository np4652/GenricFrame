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

        //public IActionResult Login()
        //{
        //    User login = new User
        //    {
        //        Username = "Jignesh"
        //    };
        //    IActionResult response = Unauthorized();
        //    var user = AuthenticateUser(login);

        //    if (user != null)
        //    {
        //        var tokenString = GenerateJSONWebToken(user);
        //        response = Ok(new { token = tokenString });
        //    }

        //    return response;
        //}
        //private string GenerateJSONWebToken(User userInfo)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //    /* Claims */
        //    var claims = new[] {
        //        new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
        //        new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
        //        new Claim("DateOfJoing", userInfo.DateOfJoing.ToString("yyyy-MM-dd")),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    };
        //    /* End Claims */
        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //      _config["Jwt:Issuer"],
        //      claims,//can be passed null instead of claims if you havn't need of claims
        //      expires: DateTime.Now.AddMinutes(120),
        //      signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //private async Task<string> GenerateJSONWebTokenAsync(User user)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //    var userRoles = new List<string> { "Admin" };//await _userManager.GetRolesAsync(user);
        //    var authClaims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.Username),
        //        new Claim(ClaimTypes.Email, user.EmailAddress),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //    };
        //    foreach (var userRole in userRoles)
        //    {
        //        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        //    }
        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //      _config["Jwt:Issuer"],
        //      authClaims,
        //      expires: DateTime.Now.AddMinutes(120),
        //      signingCredentials: credentials);
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //private User AuthenticateUser(User login)
        //{
        //    User user = new User();

        //    //Validate the User Credentials    
        //    //Demo Purpose, I have Passed HardCoded User Information    
        //    if (login.Username == "Jignesh")
        //    {
        //        user = new User { Username = "Jignesh Trivedi", EmailAddress = "test.btest@gmail.com", DateOfJoing = DateTime.Now };
        //    }
        //    return user;
        //}

        //public string Authenticate(string username, string password)
        //{
        //    var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

        //    // return null if user not found
        //    if (user == null)
        //        return null;

        //    // authentication successful so generate jwt token
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_config["JwtConfig:secret"]);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //             new Claim(ClaimTypes.Name, user.Id.ToString()),
        //        }),
        //        IssuedAt = DateTime.UtcNow,
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        //        Issuer = "Issuer",
        //        Audience = "Audience"
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        //public IActionResult AuthenticateMvc(LoginRequest request, string redirectUrl = "")
        //{
        //    var response = new Response<LoginResponse>()
        //    {
        //        StatusCode = Status.Failed,
        //        Result = new LoginResponse
        //        {
        //            IsAuthenticate = false
        //        }
        //    };
        //    var user = _users.SingleOrDefault(x => x.Username == request.UserName && x.Password == request.Password);

        //    // return null if user not found
        //    if (user == null)
        //        return Json(response);

        //    // authentication successful so generate jwt token
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_config["JWT:key"]);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //             new Claim(ClaimTypes.Name, user.Id.ToString()),
        //        }),
        //        IssuedAt = DateTime.UtcNow,
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        //        Issuer = "Issuer",
        //        Audience = "Audience"
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);

        //    //Mock response
        //    response = new Response<LoginResponse>()
        //    {
        //        StatusCode = Status.Success,
        //        ResponseText = "Success",
        //        Result = new LoginResponse
        //        {
        //            IsAuthenticate = true,
        //            Token = tokenHandler.WriteToken(token),
        //            Role = "User",
        //            RedirectUrl = redirectUrl,
        //            Claims = new List<ClaimDto>()
        //            {
        //                new ClaimDto() {Type = ClaimTypes.Role, Value = "UserRole" },
        //                new ClaimDto() {Type= ClaimTypes.Email, Value = "email@email.com" }
        //            }
        //        }
        //    };
        //    // var finalresponse = Ok(new { data = response });
        //    return Json(response);
        //}

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
            // generate token that is valid for 7 days
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
                //Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
