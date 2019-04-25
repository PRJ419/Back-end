using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Database;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Areas.Identity.Data;
using WebApi.Helpers;


namespace WebApi.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly SignInManager<BarOMeterIdentityUser> _signInManager;
        private readonly UserManager<BarOMeterIdentityUser> _userManager;


        public UserController(UserManager<BarOMeterIdentityUser> userManager,
            SignInManager<BarOMeterIdentityUser> signInManager)
        {
             _userManager = userManager;
             _signInManager = signInManager;
        }
        // POST api/Register/barrep
        [AllowAnonymous]
        [Route("api/register/barrep")]
        [HttpPost]
        public async Task<IActionResult> RegisterBarRep([FromBody] BarRepRegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new BarOMeterIdentityUser() { UserName = model.Username, Email = model.Email };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                BadRequest();
            }

            var roleClaim = new Claim("Role", "BarRep");
            await _userManager.AddClaimAsync(user, roleClaim);
            return Ok();
        }
        // POST api/Register
        [AllowAnonymous]
        [Route("api/register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new BarOMeterIdentityUser() {UserName = model.Username, Email = model.Email};

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                BadRequest();
            }
           
            var roleClaim = new Claim("Role", "Kunde");
            await _userManager.AddClaimAsync(user, roleClaim);
            return Ok();
        }

        //POST api/Login
        [AllowAnonymous]
        [Route("api/login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginBindingModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user== null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login");
                return BadRequest(ModelState);
            }

            var claims = await _userManager.GetClaimsAsync(user);
          
            TokenHelper th = new TokenHelper();
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded)
            {
                return new ObjectResult(th.GenerateToken(model.Username, claims.First().Value));
               
            }

            return BadRequest(result);
        }
        [AllowAnonymous]
        [Route("api/logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
        


    }
}
