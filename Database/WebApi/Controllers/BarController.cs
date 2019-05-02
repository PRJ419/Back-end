using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database;
using Database.Interfaces;
using Database.Repository_Implementations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using WebApi.DTOs.Bars;
using WebApi.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    /// <summary>
    /// BarController class for the Web Api.
    /// Default route: "api/bars.
    /// Can respond to various GET/ PUT/ POST/ DELETE Http requests.
    /// Returns BarSimpleDto and BarDto to client.  
    /// </summary>
    [Route("api/bars")]
    [ApiController]
    public class BarController : ControllerBase
    {
        /// <summary>
        /// Reference to UnitOfwork used for database access
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Field to store IMapper implementation.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the controller.
        /// <para>
        /// Gets the UnitOfWork for use.
        /// </para> 
        /// </summary>
        /// <param name="unitOfWork">
        /// Dependency injected through Startup.ConfigureServices()
        /// </param>
        /// <param name="mapper">
        /// IMapper implementation used to map Dto object to model objects and vice versa. 
        /// </param>
        public BarController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all Bars ranked from highest to lowest.
        /// Authorization: None
        /// </summary>
        /// <returns>
        /// Ok (200) returns a List&lt;BarSimpleDto&gt; ordered by avg ranking (descending). <para/>
        /// NotFound (404) if no bars could be found.
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<BarSimpleDto>), 200)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public IActionResult GetBestBars()
        {
            var bars = _unitOfWork.BarRepository.GetBestBars();
            var listOfBars = Converter.GenericListConvert<Bar, BarSimpleDto>(bars, _mapper);
            if (listOfBars.Any())
                return Ok(listOfBars);
            else
                return NotFound();
        }

        /// <summary>
        /// Returns a specific Bar found by provided id.
        /// Authorization: None
        /// </summary>
        /// <param name="id">
        /// is BarName property of Bar class.
        /// </param>
        /// <example>
        /// Example: "https://IP:PORT/api/bars/Katrines Kælder"
        /// </example>
        /// <returns>
        /// Ok (200) with the found Bar object if successful. <para/>
        /// NotFound (400) if the bar could not be found.
        /// </returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BarDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public IActionResult GetBar(string id)
        {
            var bar = _unitOfWork.BarRepository.Get(id);
            if (bar != null)    
            {
                var dto = _mapper.Map<BarDto>(bar);
                return Ok(dto);
            }
            else
                return NotFound();
        }

        /// <summary>
        /// Adds a Bar object to the database, if bar with same name does not exist.
        /// Authorization: Admin
        /// </summary>
        /// <param name="dtoBar">
        /// is a BarDto object supplied in the Http Body in JSON formatting. Must match property attribute rules. 
        /// </param>
        /// <returns>
        /// Created (201) if successful, and will return the created object. <para/>
        /// BadRequest (400) if unsuccessful.
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(BarDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        public IActionResult AddBar([FromBody]BarDto dtoBar)
        {
            try
            {
                _unitOfWork.BarRepository.Add(_mapper.Map<Bar>(dtoBar));
                _unitOfWork.Complete();
                return Created($"api/bars/{dtoBar.BarName}", dtoBar);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes a bar identified by id.
        /// Authorization: Admin
        /// </summary>
        /// <param name="id">
        /// string which must match a BarName.
        /// </param>
        /// <returns>
        /// Ok (200) if deletion is successful.
        /// BadRequest (400) if bar could not be found or deletion was unsuccessful.
        /// </returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public IActionResult DeleteBar(string id)
        {
            try
            {
                _unitOfWork.BarRepository.Delete(id);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates a bar if it already exists.
        /// Authorization: BarRepresentative, Admin
        /// </summary>
        /// <param name="barDto">
        /// BarDto object supplied in the Http Body in JSON formatting. <para/>
        /// Must match property attribute rules. 
        /// </param>
        /// <returns>
        /// 201 (Created) if edit was successful. <para/>
        /// 400 (BadRequest) if edit was unsuccessful. 
        /// </returns>
        [HttpPut]
        [Authorize(Roles = "BarRep,Admin")]
        [ProducesResponseType(typeof(BarDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        public IActionResult UpdateBar([FromBody]BarDto barDto)
        {
            try
            {
                var bar = _mapper.Map<Bar>(barDto);
                _unitOfWork.BarRepository.Edit(bar);
                _unitOfWork.Complete();
                return Created($"api/bars/{barDto.BarName}", barDto);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Returns a list of bars ranked from worst to best.
        /// Authorization: None
        /// </summary>
        /// <returns>
        /// Ok (200) Response and List&lt;BarDto&gt; if any found <para/>
        /// NotFound (404) Response no bars were found. 
        /// </returns>
        [AllowAnonymous]
        [HttpGet("Worst")]
        [ProducesResponseType(typeof(BarSimpleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        public IActionResult GetWorstBars()
        {
            var bars = _unitOfWork.BarRepository.GetWorstBars();
            var DtoList = Converter.GenericListConvert
                <Bar, BarSimpleDto>(bars, _mapper);
            _unitOfWork.Complete();


            if (DtoList.Any())
                return Ok(DtoList);
            else
                return NotFound();
        }

        /// <summary>
        /// Returns a range of barDto's in a List.
        /// Authorization: None 
        /// </summary>
        /// <param name="index">
        /// Start index.
        /// </param>
        /// <param name="length">
        /// How many bars to include
        /// </param>
        /// <returns>
        /// Ok (200) if found, a List&lt;BarSimpleDto&gt; picked with range as specified by the parameters. <para/>
        /// NotFound (404) if none found, and no List. 
        /// </returns>
        [AllowAnonymous]
        [HttpGet("{index}/{length}")]
        [ProducesResponseType(typeof(List<BarSimpleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public IActionResult GetRangeOfBars(int index, int length)
        {

            var bars = _unitOfWork.BarRepository.GetXBars(index, length).ToList();
            var listOfBars = Converter.GenericListConvert
                <Bar, BarSimpleDto>(bars, _mapper);
            _unitOfWork.Complete();


            if (listOfBars.Any())
                return Ok(listOfBars);
            else
                return NotFound();
        }
    }
}