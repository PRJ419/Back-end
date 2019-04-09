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
{        // TODO : AUTHENTICATE!

    [Route("api/bars/{barName}/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UnitOfWork">
        ///
        /// </param>
        public EventController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="barName">
        ///
        /// </param>
        /// 
        /// <returns>
        ///
        /// </returns>
        [HttpGet] // TODO: Query smukkesering!
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
        // TODO : AUTHENTICATE!
        [HttpPost]
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

       
        [HttpPut]
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

        [HttpDelete("{eventName}")]
        public IActionResult DeleteEvent(string eventName, string barName)
        {
            try
            {
                _unitOfWork.BarEventRepository.Delete(new[]{barName, eventName});
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