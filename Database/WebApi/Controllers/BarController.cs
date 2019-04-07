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
using WebApi.DTOs.Bars;

namespace WebApi.Controllers
{
    /// <summary>
    /// BarController class for the Web Api.
    /// Default route: "api/bars.
    ///  Can respond to various GET/ PUT/ POST/ DELETE Http requests.
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
        public async Task<IActionResult> GetBestBars()
        {
            var bars = _unitOfWork.BarRepository.GetBestBars();
            var listOfBars = BarSimpleDtoConverter.ToDtoList(bars);

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
            var bar = _unitOfWork.BarRepository.Get(id);
            
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
        /// : Bar object supplied in the Http Body in JSON formatting
        /// </param>
        /// <returns>
        /// If successful, will return the created object and code 201
        /// If unsuccessful, returns 400 (Bad Request)
        /// </returns>

        //[ValidateModel]   // remember this boi.
        [HttpPost]
        [ProducesResponseType(typeof(Bar), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBar([FromBody]BarDto dtoBar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.BarRepository.Add(BarDtoConverter.ToBar(dtoBar)); //BarDtoConverter.ToBar(dtoBar));
                    _unitOfWork.Complete();
                    return Created($"api/bars/{dtoBar.BarName}", dtoBar);
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
        /// Returns 400 Bad Request, if bar could not be found.
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBar(string id)
        {
            try
            {
                _unitOfWork.BarRepository.Delete(id);
                _unitOfWork.Complete();
                return Ok();
            }
            catch ( Exception e )
            {
                return BadRequest();
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
        public async Task<IActionResult> UpdateBar([FromBody]Bar bar)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.BarRepository.Edit(bar);
                    _unitOfWork.Complete();
                    return Ok();
                }
                catch(Exception e)
                {
                    return BadRequest();
                }
                
            }

            return BadRequest();
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
                var bars = _unitOfWork.BarRepository.GetWorstBars().ToList();
                var DtoList = BarSimpleDtoConverter.ToDtoList(bars);
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
 
                var bars = _unitOfWork.BarRepository.GetXBars(from, to).ToList();
                var listOfBars = BarSimpleDtoConverter.ToDtoList(bars);
                _unitOfWork.Complete();
            

            if (listOfBars.Any())
                return Ok(listOfBars);
            else
                return NotFound();
        }
    }
}