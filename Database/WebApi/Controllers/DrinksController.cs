using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database;
using Database.Entities;
using Database.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.DTOs;
using WebApi.DTOs.Drinks;
using WebApi.Utility;

namespace WebApi.Controllers
{
    /// <summary>
    /// Controller for drinks.
    /// Route is "api/bars/{BarName}/Drinks"
    /// </summary>
    [Route("api/bars/{BarName}/Drinks")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        /// <summary>
        /// Reference to implementation of UnitOfWork. Used for database access. 
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Field to store IMapper implementation.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for DrinksController. <para></para>
        /// Gets a IUnitOfWork by dependency injection.
        /// </summary>
        /// <param name="UnitOfWork">
        /// UnitOfWork injected through dependency injection in Startup.cs
        /// </param>
        /// <param name="mapper">
        /// IMapper implementation used to map Dto object to model objects and vice versa. 
        /// </param>
        public DrinksController(IUnitOfWork UnitOfWork, IMapper mapper)
        {
            _unitOfWork = UnitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all drinks sold by the bar.
        /// Authorization: None
        /// </summary>
        /// <param name="barName">
        /// is a string which is the id of the bar,
        /// whose drinks will be returned. 
        /// </param>
        /// <returns>
        /// Returns List&lt;DrinkDto&gt; of all the bars drinks.
        /// Ok (200) if successful. <para></para>
        /// NotFound (404) if no drinks are found. <para></para>
        /// 401 or 403 if authorization is unsuccessful. <para></para>
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<DrinkDto>), StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public IActionResult GetDrinks(string barName)
        {
            var drinks = _unitOfWork.DrinkRepository.Find(x => x.BarName == barName);
            var drinkDtoList = Converter.GenericListConvert<Drink, DrinkDto>
                (drinks, _mapper);

            if (drinkDtoList.Any())
                return Ok(drinkDtoList);
            else
                return NotFound();
        }

        /// <summary>
        /// Adds a drink to the database. 
        /// Authorization: Admin, BarRepresentative
        /// </summary>
        /// <param name="drinkDto">
        /// is a DrinkDto object version of Drink object. <para></para>
        /// Must match property attribute rules. 
        /// </param>
        /// <returns>
        /// Returns 201 (Created) on success.
        /// Returns 400 (BadRequest) on failure to insert or bad model supplied. Body will contain string: "Duplicate Key"
        /// if request failed because of duplicate key sql exception<para></para>
        /// 401 or 403 if authorization is unsuccessful. <para></para>
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "BarRep")]
        [ProducesResponseType(typeof(Drink), StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public IActionResult AddDrink([FromBody] DrinkDto drinkDto)
        {
            try
            {
                var drink = _mapper.Map<Drink>(drinkDto);
                _unitOfWork.DrinkRepository.Add(drink);
                _unitOfWork.Complete();
                return Created(string.Format($"api/bars/{drink.BarName}/Drinks"), drinkDto);
            }
            catch (Exception e)
            {
                if (e.InnerException is SqlException exception && exception.Number == 2627)
                {
                    return BadRequest("Duplicate Key");
                }
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes a drink identified by BarName and drinkName
        /// Authorization: Admin, BarRepresentative
        /// </summary>
        /// <param name="BarName">
        /// is a string which is the bars name.
        /// </param>
        /// <param name="drinkName">
        /// is a string which is the name of the drink.
        /// </param>
        /// <returns>
        /// Ok (200) on deletion <para></para>
        /// BadRequest(400) if deletion is unsuccessful.<para></para>
        /// 401 or 403 if authorization is unsuccessful. 
        /// </returns>
        [HttpDelete("{drinkName}")]
        [Authorize(Roles = "BarRep")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteDrink(string BarName, string drinkName)
        {
            try
            {
                _unitOfWork.DrinkRepository.Delete(new object[] {BarName, drinkName});
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
        /// Authorization: Admin, BarRepresentative. 
        /// </summary>
        /// <param name="drinkDto">
        /// is an updated version of a Drink object in the database. <para></para>
        /// Must match property attribute rules. 
        /// </param>
        /// <returns>
        /// Created (201) if edit was successful.<para></para>
        /// BadRequest (400) if edit was not successful.<para></para>
        /// 401 or 403 if authorization is unsuccessful. <para></para>
        /// </returns>
        [HttpPut]
        [Authorize(Roles = "BarRep")]
        [ProducesResponseType(typeof(DrinkDto), StatusCodes.Status200OK)] 
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public IActionResult EditDrink([FromBody] DrinkDto drinkDto)
        {
            try
            {
                var drink = _mapper.Map<Drink>(drinkDto);
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