using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Interfaces;
using Database.Repository_Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        private BarOMeterContext context;
        //private Repository<Bar> repo;
        /// <summary>
        /// Constructor for the controller.
        /// <para>
        /// Gets the repository for use.
        /// </para> 
        /// </summary>
        /// <param name="barRepo">
        /// Dependency injected through Startup.ConfigureServices()
        /// </param>

        public BarController(BarOMeterContext context)//IRepository<Bar> barRepo)
        {
            this.context = context;
          
        }

        /// <summary>
        /// Returns all Bars ranked from highest to lowest
        /// </summary>
        /// <returns>
        /// A List of Bars ordered by avg ranking (descending).
        /// Response codes Ok and NoContent
        /// </returns>
        [HttpGet] // /api/bars
        [ProducesResponseType(typeof(List<Bar>), 200)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetBars()
        public async Task<IActionResult> GetBestBars()
        {
            List<BarSimpleDto> listOfBars;
            using (var unitOfWork = new UnitOfWork(context))
            {
                var bars = unitOfWork.Bars.GetBestBars().ToList();
                listOfBars = BarSimpleDto.FromBarListToDtoList(bars);
                unitOfWork.Complete();
            }

            if (listOfBars.Any())
                return Ok(listOfBars);
            else
                return NoContent();

        }

        ///// <summary>
        ///// Returns a specific Bar found by provided id
        ///// </summary>
        ///// <param name="id">
        ///// id is BarName property of Bar class.
        ///// </param>
        ///// <example>
        ///// "https://IP:PORT/api/bars/Katrines Kælder"
        ///// </example>
        ///// <returns>
        ///// ActionResult Ok with the found Bar object if successful.
        ///// ActionResult NotFound if the bar could not be found.
        ///// </returns>
        ////api/bars/{id}
        //[HttpGet("{id}")]
        //[ProducesResponseType(typeof(Bar), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetBar(string id)
        //{
        //    using (var unitOfWork = new UnitOfWork(context))
        //    {
        //        unitOfWork.Bars.Find()
        //    }
        //        var bar = await db.GetBar(id);
        //    if (bar != null)
        //        return Ok(bar);
        //    else
        //        return NotFound($"Bar with BarName: {id} not found");
        //}

        // TODO : Mangler REPO implementering
        ///// <summary>
        ///// Adds a Bar object to the database, if bar with same name does not exist
        ///// </summary>
        ///// <param name="bar">
        ///// Bar object supplied in the Http Body in JSON formatting
        ///// </param>
        ///// <returns>
        ///// If successful, will return the created object with /api/bars/{id}
        ///// If unsuccessful, returns 400 (Bad Request)
        ///// </returns>
        //[HttpPost]
        ////[ValidateModel]   // remember this boi.
        //[ProducesResponseType(typeof(Bar), StatusCodes.Status201Created)]
        //[ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> AddBar([FromBody]Bar bar)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (var unitOfWork = new UnitOfWork(context))
        //        {
        //            var barToAdd = new Bar()
        //            {
        //                Address = bar.Address, AgeLimit = bar.AgeLimit, AvgRating = bar.AvgRating,
        //                BarEvents = bar.BarEvents, BarName = bar.BarName, Barrepresentatives = bar.Barrepresentatives,
        //                Coupons = bar.Coupons, CVR = bar.CVR, Drinks = bar.Drinks, Educations = bar.Educations,
        //                LongDescription = bar.LongDescription, Email = bar.Email, PhoneNumber = bar.PhoneNumber,
        //                Reviews = bar.Reviews, ShortDescription = bar.ShortDescription,
        //            };
        //            unitOfWork.Bars.Add(barToAdd);
        //            unitOfWork.Complete();
        //            //return CreatedAtAction(nameof(Find), new { id = bar.BarName }, bar);
        //            return Ok(bar);
        //        }
        //    }
        //    else return BadRequest();


        //    //var b = new Bar();
        //    //b.BarName = bar.BarName;
        //    //b.Rating = bar.Rating;
        //    //bool success = await db.AddBar(b);
        //    //if (success)
        //    //    return CreatedAtAction(nameof(GetBar), new { id = bar.BarName }, bar);
        //    //else
        //    //{
        //    //    return BadRequest();
        //    //}
        //}

        ///// <summary>
        ///// Deletes a bar identified by id
        ///// </summary>
        ///// <param name="id">
        ///// string id which must match a BarName
        ///// </param>
        ///// <returns>
        ///// Returns 200 Ok if deletion is successful.
        ///// Returns 404 Not found, if bar could not be found.
        ///// </returns>
        //[HttpDelete("{id}")]
        //[ProducesResponseType(typeof(Nullable), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> DeleteBar(string id)
        //{
        //    bool result = await db.RemoveBar(id);
        //    if (result)
        //        return Ok();
        //    return NotFound(id);
        //}

        ///// <summary>
        ///// Updates a bar if it already exists.
        ///// </summary>
        ///// <param name="bar">
        ///// Bar object supplied in the Http Body in JSON formatting.
        ///// Must include "BarName": string and "Rating": int
        ///// </param>
        ///// <returns></returns>
        //[HttpPut]
        //[ProducesResponseType(typeof(Bar), StatusCodes.Status202Accepted)]
        //[ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> UpdateRating([FromBody]Bar bar)
        //{
        //    // Skal lige laves ordenligt udfra EFCore
        //    return Unauthorized("Ez lol");
        //}

        /// <summary>
        /// Returns a list of bars ranked from worst to best
        /// </summary>
        /// <returns>
        /// Ok: 200 Response and list of BarDto if any found
        /// NoContent: 204 Response if not found
        /// </returns>
        [HttpGet("Worst")]
        [ProducesResponseType(typeof(Bar), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetWorstBars()
        {
            List<BarSimpleDto> DtoList;

            using (var unitOfWork = new UnitOfWork(context))
            {
                var bars = unitOfWork.Bars.GetWorstBars().ToList();
                DtoList = BarSimpleDto.FromBarListToDtoList(bars);
                unitOfWork.Complete();
            }

            if (DtoList.Any())
                return Ok(DtoList);
            else
                return NoContent();
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
        /// If none found: NoContent(204) and no list
        /// </returns>
        [HttpGet("{from}/{to}")]
        [ProducesResponseType(typeof(Bar), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetRangeOfBars(int from, int to)
        {
            List<BarSimpleDto> listOfBars;
            using (var unitOfWork = new UnitOfWork(context))
            {
                var bars = unitOfWork.Bars.GetXBars(from, to).ToList();
                listOfBars = BarSimpleDto.FromBarListToDtoList(bars);
                unitOfWork.Complete();
            }

            if (listOfBars.Any())
                return Ok(listOfBars);
            else
                return NoContent();
        }
    }
}