using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Identity;
using WebApi.Models;
using Microsoft.Net.Http;
using System.Web.Http.ModelBinding;

namespace WebApi.Controllers
{
    public class UserController
    {
        // POST api/Account/Register
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [Microsoft.AspNetCore.Mvc.Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            var user = new ApplicationUser() {UserName = model.Email, Email = model.Email};

            IdentityResult result = await.
        }
       
    
        


    }
}
