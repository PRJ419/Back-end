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
    /// Route is api/bars/{BarName}/Drinks.
    /// </summary>
    [Route("api/bars/{BarName}/Drinks")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        /// <summary>
        /// Reference to implementation of UnitOfWork
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor for DrinksController.
        /// Gets a IUnitOfWork by dependency injection.
        /// </summary>
        /// <param name="UnitOfWork">
        /// UnitOfWork injected through dependency injection
        /// </param>
        public DrinksController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        /// <summary>
        /// Returns all drinks sold by the bar.
        /// </summary>
        /// <param name="barName">
        /// : barName is the id of the bar,
        /// whose drinks will be returned. 
        /// </param>
        /// <returns>
        /// Returns List&lt;DrinkDto&gt; of all the bars drinks.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<DrinkDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public IActionResult GetDrinks(string barName)
        {
            var drinks = _unitOfWork.DrinkRepository.Find(x => x.BarName == barName).ToList();
            var drinkDtos = DrinkDtoConverter.ToDtoList(drinks);

            if (drinkDtos.Any())
                return Ok(drinkDtos);
            else
                return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Drink), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        public IActionResult AddDrink([FromBody] DrinkDto drinkDto)
        {
            try
            {
                var drink = DrinkDtoConverter.ToDrink(drinkDto);
                _unitOfWork.DrinkRepository.Add(drink);
                _unitOfWork.Complete();
                return Created(string.Format($"api/bars/{drink.BarName}/Drinks"), drinkDto);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        public IActionResult DeleteDrink([FromBody] DrinkDto drinkDto)
        {
            try
            {
                // This is the key of a Drink. 
                string[] key = new string[2];
                key[0] = drinkDto.BarName;
                key[1] = drinkDto.DrinksName;

                _unitOfWork.DrinkRepository.Delete(key);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(DrinkDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        public IActionResult EditDrink([FromBody] DrinkDto drinkDto)
        {
            try
            {
                var drink = DrinkDtoConverter.ToDrink(drinkDto);
                _unitOfWork.DrinkRepository.Edit(drink);
                _unitOfWork.Complete();
                return Created(string.Format($"api/bars/{drink.BarName}/Drinks"), drinkDto);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


    }
}