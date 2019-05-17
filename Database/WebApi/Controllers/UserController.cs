using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Database;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Areas.Identity.Data;
using WebApi.DTOs.BarRepresentative;
using WebApi.DTOs.Bars;
using WebApi.DTOs.Customers;
using WebApi.Helpers;


namespace WebApi.Controllers
{
    /// <summary>
    /// UserController for the Web Api. 
    /// Used to register users, bars and admins and log them in. 
    /// </summary>
    public class UserController : ControllerBase
    {
        private readonly SignInManager<BarOMeterIdentityUser> _signInManager;
        private readonly UserManager<BarOMeterIdentityUser> _userManager;
        private readonly CustomerController _customerController;
        private readonly BarController _barController;
        private readonly BarRepresentativeController _barRepresentativeController;


        public UserController(UserManager<BarOMeterIdentityUser> userManager,
            SignInManager<BarOMeterIdentityUser> signInManager,
            CustomerController customerController,
            BarController barController,
            BarRepresentativeController barRepresentativeController)
        {
             _userManager = userManager;
             _signInManager = signInManager;
             _customerController = customerController;
             _barController = barController;
             _barRepresentativeController = barRepresentativeController;
        }

        /// <summary>
        /// Registers a Bar Representative and adds the bar to the database. Authorization: None.
        /// </summary>
        /// <param name="model">
        /// Is at model with all the data required to register a bar representative and a bar
        /// </param>
        /// <returns>
        /// Ok (200) if registration was successful. <para></para>
        /// BadRequest (400) if registration is not successful.<para></para>
        /// </returns>
        // POST api/Register/barrep
        [AllowAnonymous]
        [Route("api/register/barrep")]
        [ProducesResponseType(typeof(Nullable), 200)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> RegisterBarRep([FromBody] BarRepRegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new BarOMeterIdentityUser() { UserName = model.Username, Email = model.Email };


            var barResult = _barController.AddBar(new BarDto
            {
                BarName = model.BarName,
                Address = model.Address,
                AgeLimit = model.AgeLimit,
                AvgRating = model.AvgRating,
                CVR = model.CVR,
                Educations = model.Educations,
                Email = model.Email,
                Image = model.Image,
                LongDescription = model.LongDescription,
                PhoneNumber = model.PhoneNumber,
                ShortDescription = model.ShortDescription
            });

            if (!(barResult is CreatedResult))
            {
                // if error rollback claim
                var list = new List<string>();
                list.Add("Bar could not be created");
                return BadRequest(list);

            }

            var addResult = _barRepresentativeController.AddBarRepresentative(new BarRepresentativeDto
            {
                Name = model.Name,
                Username = model.Username,
                BarName = model.BarName

            });

          
            if (!(addResult is CreatedResult))
            {
                // if error rollback bar and claim
                var list = new List<string>();
                list.Add("BarRepresentative exists");
                _barController.DeleteBar(model.BarName);
                return BadRequest(list);
            }

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var sanitizedList = new List<string>();
                foreach (var error in result.Errors)
                {
                    if (error.Code == "DuplicateUserName" || error.Code == "DuplicateEmail" || error.Code == "PasswordRequiresUpper" || error.Code == "PasswordTooShort" || error.Code == "PasswordRequiresLower" || error.Code == "PasswordRequiresDigit")
                        sanitizedList.Add(error.Code);

                }

                _barController.DeleteBar(model.BarName);
                _barRepresentativeController.DeleteBarRepresentative(model.Username);

                return BadRequest(sanitizedList);

            }
            var roleClaim = new Claim("Role", "BarRep");
            await _userManager.AddClaimAsync(user, roleClaim);

            return Ok();
        }
        /// <summary>
        /// Registers an administrator. Authorization: Admin.
        /// </summary>
        /// <param name="model">
        /// Is at model with all the data required to register an admin representative
        /// </param>
        /// <returns>
        /// Ok (200) if registration was successful. <para></para>
        /// BadRequest (400) if registration is not successful.<para></para>
        /// </returns>
        // POST api/Register/admin
        [Authorize(Roles = "Admin")]
        [Route("api/register/Admin")]
        [SwaggerResponse(StatusCodes.Status200OK)]
 
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterBindingModel model )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new BarOMeterIdentityUser() { UserName = model.Username, Email = model.Email };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            var roleClaim = new Claim("Role", "Admin");
            await _userManager.AddClaimAsync(user, roleClaim);
          
                return Ok();
     
           
        }
        /// <summary>
        /// Registers a user to the database Authorization: None.
        /// </summary>
        /// <param name="model">
        /// Is at model with all the data required to register a user
        /// </param>
        /// <returns>
        /// Ok (200) if registration was successful. <para></para>
        /// BadRequest (400) if registration is not successful.<para></para>
        /// </returns>
        // POST api/Register
        [AllowAnonymous]
        [Route("api/register")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]

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
                var sanitizedList = new List<string>();
                foreach (var error in result.Errors)
                {
                    if (error.Code == "DuplicateUserName" ||error.Code == "DuplicateEmail"|| error.Code == "PasswordRequiresUpper" || error.Code == "PasswordTooShort" || error.Code == "PasswordRequiresLower" || error.Code == "PasswordRequiresDigit")
                        sanitizedList.Add(error.Code);
                    
                }

               return BadRequest(sanitizedList);
            }
           
            var roleClaim = new Claim("Role", "Kunde");
            await _userManager.AddClaimAsync(user, roleClaim);


           var addResult = _customerController.AddCustomer(new CustomerDto
            {
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                Name = model.Name,
                FavoriteBar = model.FavoriteBar,
                FavoriteDrink = model.FavoriteDrink,
                Username = model.Username,

            });

           if (addResult is CreatedResult)
               return Ok();
           else
               await _userManager.RemoveClaimAsync(user, roleClaim);
               return BadRequest();
        }

        /// <summary>
        /// Enables logins to the application. Authorization: None.
        /// </summary>
        /// <param name="model">
        /// Is at model with all the data required to login the given account
        /// </param>
        /// <returns>
        /// Ok (200) if login was successful. <para></para>
        /// BadRequest (400) if login is not successful.<para></para>
        /// </returns>
        //POST api/Login
        [AllowAnonymous]
        [Route("api/login")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
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
                return BadRequest("Invalid login");
            }

            var claims = await _userManager.GetClaimsAsync(user);
          
            TokenHelper th = new TokenHelper();
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded)
            {
                return new ObjectResult(th.GenerateToken(model.Username, claims.First().Value));
               
            }

            return BadRequest("Invalid login");
        }

    }
}
