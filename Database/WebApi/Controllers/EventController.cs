using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Database.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.BarEvent;

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
        /// Constructor for the controller. <para/>
        /// Gets a IUnitOfWork by dependency injection (configured in Startup.cs)
        /// </summary>
        /// <param name="unitOfWork">
        /// UnitOfWork implementation used for database access. 
        /// </param>
        public EventController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Returns all events associated with a Bar. 
        /// </summary>
        /// <param name="barName">
        /// string identifying the bar. 
        /// </param>
        /// <returns>
        /// Ok (200) and a List&lt;BarEventDto&gt; of all BarEventDto's associated with that Bar. <para></para>
        /// NotFound (404) if no events were found. 
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<BarEventDto>), 200)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public IActionResult GetEvents(string barName)
        {
            var events = _unitOfWork.BarEventRepository.Find(x => x.BarName == barName);
            var dtoEvents = BarEventDtoConverter.ToDtoList(events);
            if (dtoEvents.Any())
            {
                return Ok(dtoEvents);
            }
            else
                return NotFound();
        }

        /// <summary>
        /// Adds a BarEvent to the database. 
        /// </summary>
        /// <param name="eventDto">
        /// is a BarEventDto object. Must match property attribute rules. 
        /// </param>
        /// <returns>
        /// Created (201) if BarEvent was added. <para></para>
        /// BadRequest (400) if model requirements weren't. 
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(BarEventDto), 201)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public IActionResult AddEvent([FromBody] BarEventDto eventDto)
        {
            try
            {
                var barEvent = BarEventDtoConverter.ToBarEvent(eventDto);
                _unitOfWork.BarEventRepository.Add(barEvent);
                _unitOfWork.Complete();
                return Created(string.Format($"api/bars/{barEvent.BarName}/events"), eventDto);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Edits an BarEvent
        /// </summary>
        /// <param name="eventDto">
        /// is a BarEventDto which holds edited data. <para></para>
        /// Must hold a BarName and EventName which can be found in the database.  <para></para>
        /// Must match property attribute rules. 
        /// </param>
        /// <returns>
        /// Created (201) if edit was successful. <para></para>
        /// BadRequest (404) if edit was unsuccessful. See parameter requirements. 
        /// </returns>
        [HttpPut]
        [ProducesResponseType(typeof(BarEventDto), 201)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        public IActionResult EditEvent([FromBody] BarEventDto eventDto)
        {
            try
            {
                var barEvent = BarEventDtoConverter.ToBarEvent(eventDto);
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
        /// </returns>
        [HttpDelete("{eventName}")]
        [ProducesResponseType(typeof(Nullable), 200)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
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