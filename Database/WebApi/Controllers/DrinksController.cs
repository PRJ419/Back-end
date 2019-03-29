using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.DTOs.Drinks;

namespace WebApi.Controllers
{
    /// <summary>
    /// Controller for drinks.
    /// Route is api/bars/{BarName}/Drinks
    /// </summary>
    [Route("api/bars/{BarName}/Drinks")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        /// <summary>
        /// Constructor for DrinksController.
        /// Gets a IUnitOfWork by dependency injection.
        /// </summary>
        /// <param name="UnitOfWork">
        /// UnitOfWork injected through Startup.cs 
        /// </param>
        public DrinksController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        /// <summary>
        /// Returns all drinks sold by the bar.
        /// </summary>
        /// <param name="BarName">
        /// BarName is the id of the bar,
        /// which drinks will be returned. 
        /// </param>
        /// <returns>
        /// Returns List&lt;DrinkDto&gt; of all the bars drinks.
        /// </returns>
      
        [HttpGet]
        [ProducesResponseType(typeof(List<DrinkDto>), 200)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDrinks(string barName)
        {
            var drinks = _unitOfWork.DrinkRepository.Find(x => x.BarName == barName).ToList();
            var drinkDtos = DrinkDtoConverter.ToDtoList(drinks);

            if (drinkDtos.Any())
                return Ok(drinkDtos);
            else
                return NotFound();
        }
    }
}