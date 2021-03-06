using GenricFrame.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly UserManager<AppicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<AppicationUser> _signInManager;
        public AccountController(IConfiguration config, IOptions<AppSettings> appSettings, UserManager<AppicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<AppicationUser> signInManager)
        {
            _config = config;
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        private List<AppicationUser> _users = new List<AppicationUser>
         {
             new AppicationUser { Id = 1, UserName = "Amit", PasswordHash = "password" },
             new AppicationUser { Id = 2, UserName = "test", PasswordHash = "test" }
         };

        [HttpGet]
        public IActionResult Register()
        {

            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = new AppicationUser { UserId = Guid.NewGuid().ToString(), UserName = model.EmailId, Email = model.EmailId, Role = model.RoleName };
            var res = await _userManager.CreateAsync(user, model.Password);
            if (res.Succeeded)
            {
                user = _userManager.FindByEmailAsync(user.Email).Result;
                await _userManager.AddToRoleAsync(user, model.RoleName);
                ModelState.AddModelError("", "Register Successfully.");
            }
            else
            {
                foreach (var error in res.Errors)
                {
                    ModelState.TryAddModelError("", error.Description);
                }
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null, bool IsAPIRequest = false)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            var result = await _signInManager.PasswordSignInAsync(model.EmailId, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                if(!IsAPIRequest)
                    return LocalRedirect(returnUrl);

                var user = _userManager.FindByEmailAsync(model.EmailId).Result;
                //if (user == null) return null;

                // authentication successful so generate jwt token
                var token = generateJwtToken(user);
                return Json(new AuthenticateResponse(user, token));
                
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            return LocalRedirect(returnUrl);

        }
        public IActionResult Users()
        {

            var users = _userManager.Users.ToList();

            return View(users);

        }
        /* JWT */
        #region JWT
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginRequest model)
        {
            var response = Authenticate(model.UserName, model.Password);
            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(response);
        }
        // helper methods
        private AuthenticateResponse Authenticate(string userName, string password)
        {
            var user = _users.SingleOrDefault(x => x.UserName == userName && x.PasswordHash == password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }
        private string generateJwtToken(AppicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            /* Claims */
            var claims = new[] {
                new Claim("id", user.Id.ToString()),
                new Claim("role", "Admin"),
                new Claim("userName", user.UserName),
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
        #endregion
        /* End */
    }
}