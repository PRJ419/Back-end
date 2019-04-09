using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        /// : string which is the id of the bar,
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

        /// <summary>
        /// Adds a drink to the database. 
        /// </summary>
        /// <param name="drinkDto">
        /// : Dto version of Drink.
        /// Must be valid object, or else an exception will be thrown on insertion in database. 
        /// </param>
        /// <returns>
        /// Returns 201 (Created) on success.
        /// Returns 400 (BadRequest) on failure to insert. 
        /// </returns>
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

        /// <summary>
        /// Deletes a drink identified by BarName and drinkName
        /// </summary>
        /// <param name="BarName">
        /// : string which is the bars name.
        /// </param>
        /// <param name="drinkName">
        /// : string which is the name of the drink.
        /// </param>
        /// <returns>
        /// Returns 200 (Ok) on deletion
        /// Returns 400 (BadRequest) if deletion is unsuccessful.
        /// </returns>
        [HttpDelete("{drinkName}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        public IActionResult DeleteDrink(string BarName, string drinkName)
        {
            try
            {
                _unitOfWork.DrinkRepository.Delete(new []{BarName, drinkName});
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Edit a drink.
        /// </summary>
        /// <param name="drinkDto">
        /// : updated version of a Drink object in the database
        /// </param>
        /// <returns>
        /// 200 (Ok) if edit was successful.
        /// 400 (BadRequest) if edit was not successful.
        /// </returns>
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