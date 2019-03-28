using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Interfaces;
using Database.Repository_Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using WebApi.DTOs;

namespace BarOMeterWebApiCore.Controllers
{
    /// <summary>
    /// BarController class for the Wen Api.
    /// <para>
    /// Default route: "api/bars.
    /// </para>
    /// <para>
    ///  Can respond to various GET/ PUT/ POST/ DELETE Http requests.
    /// </para>
    /// </summary>
    [Route("api/bars")]
    [ApiController]
    public class BarController : ControllerBase
    {

        private IUnitOfWork _unitOfWork;
        //private Repository<Bar> repo;
        /// <summary>
        /// Constructor for the controller.
        /// <para>
        /// Gets the repository for use.
        /// </para> 
        /// </summary>
        /// <param name="unitOfWork">
        /// Dependency injected through Startup.ConfigureServices()
        /// </param>
        public BarController(IUnitOfWork unitOfWork)//IRepository<Bar> barRepo)
        {
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Returns all Bars ranked from highest to lowest
        /// </summary>
        /// <returns>
        /// A List of Bars ordered by avg ranking (descending).
        /// Response codes Ok(200) and NotFound(404)
        /// </returns>
        [HttpGet] // /api/bars
        [ProducesResponseType(typeof(List<BarSimpleDto>), 200)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetBars()
        public async Task<IActionResult> GetBestBars()
        {
                var bars = _unitOfWork.Bars.GetBestBars().ToList();
                var listOfBars = BarSimpleDto.FromBarListToDtoList(bars);
                _unitOfWork.Complete();

                if (listOfBars.Any())
                    return Ok(listOfBars);
                else
                    return NotFound();
        }

        /// <summary>
        /// Returns a specific Bar found by provided id
        /// </summary>
        /// <param name="id">
        /// id is BarName property of Bar class.
        /// </param>
        /// <example>
        /// "https://IP:PORT/api/bars/Katrines Kælder"
        /// </example>
        /// <returns>
        /// ActionResult Ok with the found Bar object if successful.
        /// ActionResult NotFound if the bar could not be found.
        /// </returns>
        //api/bars/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Bar), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBar(string id)
        {
            var bar = _unitOfWork.Bars.Get(id);
            
            if (bar != null)
                return Ok(bar);
            else
                return NotFound();
        }

        // TODO : Mangler REPO implementering
        /// <summary>
        /// Adds a Bar object to the database, if bar with same name does not exist
        /// </summary>
        /// <param name="bar">
        /// Bar object supplied in the Http Body in JSON formatting
        /// </param>
        /// <returns>
        /// If successful, will return the created object with /api/bars/{id}
        /// If unsuccessful, returns 400 (Bad Request)
        /// </returns>

        //[ValidateModel]   // remember this boi.
        [HttpPost]
        [ProducesResponseType(typeof(Bar), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBar([FromBody]BarDto bar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.Bars.Add(BarDto.ConvertToBar(bar));
                    _unitOfWork.Complete();
                    return Created($"api/bars/{bar.BarName}", bar);
                }
                else
                    return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.HResult);
            }
        }

        /// <summary>
        /// Deletes a bar identified by id
        /// </summary>
        /// <param name="id">
        /// string id which must match a BarName
        /// </param>
        /// <returns>
        /// Returns 200 Ok if deletion is successful.
        /// Returns 404 Not found, if bar could not be found.
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBar(string id)
        {
            try
            {
                _unitOfWork.Bars.Delete(id);
                _unitOfWork.Complete();
                return Ok();
            }
            catch ( Exception e )
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Updates a bar if it already exists.
        /// </summary>
        /// <param name="bar">
        /// Bar object supplied in the Http Body in JSON formatting.
        /// Must include "BarName": string and "Rating": int
        /// </param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Bar), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRating([FromBody]Bar bar)
        {
            // Skal lige laves ordenligt udfra EFCore
            return Unauthorized("Ez lol");
        }

        /// <summary>
        /// Returns a list of bars ranked from worst to best
        /// </summary>
        /// <returns>
        /// Ok: 200 Response and list of BarDto if any found
        /// NotFound: 404 Response if not found
        /// </returns>
        [HttpGet("Worst")]
        [ProducesResponseType(typeof(Bar), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetWorstBars()
        {
                var bars = _unitOfWork.Bars.GetWorstBars().ToList();
                var DtoList = BarSimpleDto.FromBarListToDtoList(bars);
                _unitOfWork.Complete();
            

            if (DtoList.Any())
                return Ok(DtoList);
            else
                return NotFound();
        }

        /// <summary>
        /// Returns a range of barDto's
        /// </summary>
        /// <param name="from">
        /// Start index
        /// </param>
        /// <param name="to">
        /// End index
        /// </param>
        /// <returns>
        /// If found: Ok(200) and all BarDto's in the range [from : to] in the database
        /// If none found: NotFound(404) and no list
        /// </returns>
        [HttpGet("{from}/{to}")]
        [ProducesResponseType(typeof(Bar), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRangeOfBars(int from, int to)
        {
 
                var bars = _unitOfWork.Bars.GetXBars(from, to).ToList();
                var listOfBars = BarSimpleDto.FromBarListToDtoList(bars);
                _unitOfWork.Complete();
            

            if (listOfBars.Any())
                return Ok(listOfBars);
            else
                return NotFound();
        }
    }
}