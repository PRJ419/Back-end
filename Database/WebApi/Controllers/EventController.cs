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
    [Route("api/bars/{barName}/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public EventController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

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

        // TODO : Der er noget fucky wucky med key'en. 
        [HttpPut] // TODO: QUERY SMUKKESERING!
        public IActionResult EditEvent([FromBody] BarEventDto eventDto)
        {
            try
            {
                var barEvent = _unitOfWork.BarEventRepository.Get(new[] {eventDto.BarName, eventDto.EventName});
                barEvent.Date = eventDto.Date;
                barEvent.EventName = eventDto.EventName;
                //var barEvent = BarEventDtoConverter.ToBarEvent(eventDto);
                //_unitOfWork.BarEventRepository.Edit(barEvent);
                _unitOfWork.Complete();
                return Created(string.Format($"api/bars/{barEvent.BarName}/events"), eventDto);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("/{eventName}")]
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