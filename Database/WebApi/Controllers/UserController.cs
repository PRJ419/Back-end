using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Identity;
using WebApi.Models;
using Microsoft.Net.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.Areas.Identity.Data;

namespace WebApi.Controllers
{
    public class UserController : ApiController
    {
        private readonly SignInManager<BarOMeterIdentityUser> _signInManager;
        private readonly UserManager<BarOMeterIdentityUser> _userManager;


        public UserController(UserManager<BarOMeterIdentityUser> userManager,
            SignInManager<BarOMeterIdentityUser> signInManager)
        {
             _userManager = userManager;
             _signInManager = signInManager;
        }
        // POST api/Account/Register
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [Microsoft.AspNetCore.Mvc.Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new BarOMeterIdentityUser() {UserName = model.Email, Email = model.Email};

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                BadRequest();
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok();
        }
       
    
        


    }
}
