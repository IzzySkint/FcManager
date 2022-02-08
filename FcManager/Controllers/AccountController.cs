using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FcManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace FcManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        
        public AccountController(ILogger<AccountController> logger, 
            IConfiguration configuration,
            SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager)
        {
            this._logger = logger;
            this._configuration = configuration;
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        [HttpPost]
        [Route("createUser")]
        [Authorize(Roles = "Root")]
        public async Task<IActionResult> CreateUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new IdentityUser
                    {
                        UserName = model.UserName,
                        Email = model.Email
                    };

                    var userCreateResult = await _userManager.CreateAsync(user, model.Password);

                    if (userCreateResult.Succeeded)
                    {
                        var roleAddResult = await _userManager.AddToRoleAsync(user, "Root");

                        if (roleAddResult.Succeeded)
                        {
                            return Ok();
                        }
                    }
                    else
                    {
                        foreach (var e in userCreateResult.Errors)
                        {
                            ModelState.AddModelError("", e.Description);
                        }

                        return BadRequest(ModelState.Values);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error creating user");
                    ModelState.AddModelError("", e.Message);
                    return BadRequest(ModelState.Values);
                }
            }
            
            return BadRequest(ModelState.Values);
        }
        
        [Route("authenticate")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(AuthenticateModel model)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

                if (signInResult.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    var roles = await _userManager.GetRolesAsync(user);

                    IdentityOptions identityOptions = new IdentityOptions();
                    var claims = new Claim[]
                    {
                        new Claim(identityOptions.ClaimsIdentity.UserIdClaimType, user.Id),
                        new Claim(identityOptions.ClaimsIdentity.UserNameClaimType, user.UserName),
                        new Claim(identityOptions.ClaimsIdentity.RoleClaimType, roles[0])
                    };

                    var signingKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));
                    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                    var jwt = new JwtSecurityToken(signingCredentials: signingCredentials,
                        claims: claims, expires: DateTime.Now.AddSeconds(30));

                    var result = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(jwt),
                        UserId = user.Id,
                        UserName = user.UserName,
                        Role = roles[0]
                    };

                    return Ok(result);
                }
            }

            return BadRequest(ModelState.Values);
        }
    }
}