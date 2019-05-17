using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Database;
using Database.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.BarEvent;
using WebApi.Utility;

namespace WebApi.Controllers
{

    /// <summary>
    /// Web Api Controller for BarEvents.<para/>
    /// Returns BarEventDto objects
    /// </summary>
    [Route("api/bars/{barName}/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        /// <summary>
        /// Reference to unit of work, used for database access. 
        /// </summary>
        private IUnitOfWork _unitOfWork;
        
        /// <summary>
        /// Field to store IMapper implementation.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the controller. <para/>
        /// Gets a IUnitOfWork by dependency injection (configured in Startup.cs)
        /// </summary>
        /// <param name="unitOfWork">
        /// UnitOfWork implementation used for database access. 
        /// </param>
        /// <param name="mapper">
        /// IMapper implementation used to map Dto object to model objects and vice versa. 
        /// </param>
        public EventController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper; 
        }

        /// <summary>
        /// Returns all events associated with a Bar. 
        /// Authorization: None
        /// </summary>
        /// <param name="barName">
        /// string identifying the bar. 
        /// </param>
        /// <returns>
        /// Ok (200) and a List&lt;BarEventDto&gt; of all BarEventDto's associated with that Bar. <para></para>
        /// NotFound (404) if no events were found. 
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<BarEventDto>), 200)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public IActionResult GetEvents(string barName)
        {
            var events = _unitOfWork.BarEventRepository.Find(x => x.BarName == barName);
            var dtoEvents = Converter.GenericListConvert<BarEvent, BarEventDto>(events, _mapper);

            if (dtoEvents.Any())
            {
                return Ok(dtoEvents);
            }
            else
                return NotFound();
        }

        /// <summary>
        /// Adds a BarEvent to the database. 
        /// Authorization: Admin, BarRepresentative
        /// </summary>
        /// <param name="eventDto">
        /// is a BarEventDto object. Must match property attribute rules. 
        /// </param>
        /// <returns>
        /// Created (201) if BarEvent was added. <para></para>
        /// BadRequest (400) if model requirements weren't. Body will contain string: "Duplicate Key"
        /// if request failed because of duplicate key sql exception
        /// Unauthorized (401) if authentication is unsuccessful. <para></para>
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "BarRep,Admin")]
        [ProducesResponseType(typeof(BarEventDto), 201)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        public IActionResult AddEvent([FromBody] BarEventDto eventDto)
        {
            try
            {
                var barEvent = _mapper.Map<BarEvent>(eventDto);
                _unitOfWork.BarEventRepository.Add(barEvent);
                _unitOfWork.Complete();
                return Created(string.Format($"api/bars/{barEvent.BarName}/events"), eventDto);
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
        /// Edits an BarEvent.
        /// Authorization: Admin, BarRepresentative
        /// </summary>
        /// <param name="eventDto">
        /// is a BarEventDto which holds edited data. <para></para>
        /// Must hold a BarName and EventName which can be found in the database.  <para></para>
        /// Must match property attribute rules. 
        /// </param>
        /// <returns>
        /// Created (201) if edit was successful. <para></para>
        /// BadRequest (400) if edit was unsuccessful. See parameter requirements.
        /// Unauthorized (401) if authentication is unsuccessful. <para></para>
        /// </returns>
        [HttpPut]
        [Authorize(Roles = "BarRep,Admin")]
        [ProducesResponseType(typeof(BarEventDto), 201)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        public IActionResult EditEvent([FromBody] BarEventDto eventDto)
        {
            try
            {
                var barEvent = _mapper.Map<BarEvent>(eventDto);
                _unitOfWork.BarEventRepository.Edit(barEvent);
                _unitOfWork.Complete();
                return Created(string.Format($"api/bars/{barEvent.BarName}/events"), eventDto);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes an event. 
        /// Authorization: Admin, BarRepresentative
        /// </summary>
        /// <param name="eventName">
        /// is a string holding the event name
        /// </param>
        /// <param name="barName">
        /// is a string holding the name of the bar. 
        /// </param>
        /// <returns>
        /// Ok (200) if deletion was successful. <para></para>
        /// BadRequest (400) if deletion was unsuccessful.
        /// Unauthorized (401) if authentication is unsuccessful. <para></para>
        /// </returns>
        [HttpDelete("{eventName}")]
        [Authorize(Roles = "BarRep,Admin")]
        [ProducesResponseType(typeof(Nullable), 200)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteEvent(string eventName, string barName)
        {
            try
            {
                _unitOfWork.BarEventRepository.Delete(new object[] { barName, eventName });
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
    }
}