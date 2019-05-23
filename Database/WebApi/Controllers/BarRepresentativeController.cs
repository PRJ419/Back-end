using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database;
using Database.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.DTOs.BarRepresentative;
using WebApi.DTOs.Customers;
using WebApi.Utility;

namespace WebApi.Controllers
{
    /// <summary>
    /// Web Api Controller for BarRepresentatives.<para/>
    /// Route is "api/BarRepresentatives" <para></para>
    /// Returns BarRepresentativeDto objects <para></para>
    /// </summary>
    [Route("api/BarRepresentatives")]
    [ApiController]
    public class BarRepresentativeController : ControllerBase
    {
        
        /// <summary>
        /// Reference to a IUnitOfWork implementation, used for database access. 
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
        public BarRepresentativeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all BarRepresentatives.
        /// Authorization: Admin
        /// </summary>
        /// <returns>
        /// Ok (200) and a List&lt;BarRepresentativeDto&gt; of all BarRepresentatives<para></para>
        /// NotFound (404) if no BarRepresentatives were found.<para></para>
        /// 401 or 403 if authorization is unsuccessful. <para></para>
        /// </returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<BarRepresentativeDto>), StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public IActionResult GetBarRepresentatives()
        {
            var barRepList= _unitOfWork.BarRepRepository.GetAll().ToList();
            var barRepDtoList = Converter.GenericListConvert
                <BarRepresentative, BarRepresentativeDto>(barRepList, _mapper);

            if (barRepDtoList.Any())
            {
                return Ok(barRepDtoList);
            }
            else
                return NotFound();
        }

        /// <summary>
        /// Returns a BarRepresentativeDto found by username
        /// Authorization: Admin, BarRepresentative
        /// </summary>
        /// <param name="username">
        /// is a string identifying the key of the BarRepresentative. 
        /// </param>
        /// <returns>
        /// Ok (200) and a BarRepresentativeDto object equivalent to the
        /// BarRepresentative saved in the database if found.<para></para>
        /// NotFound (404) if the BarRepresentative was not found.<para></para>
        /// 401 or 403 if authorization is unsuccessful. <para></para>
        /// </returns>
        [HttpGet("{username}")]
        [Authorize(Roles = "BarRep,Admin")]
        [ProducesResponseType(typeof(BarRepresentativeDto), StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public IActionResult GetBarRepresentative(string username)
        {
            var barRep = _unitOfWork.BarRepRepository.Get(username);

            if (barRep == null)
                return NotFound();

            return Ok(_mapper.Map<BarRepresentativeDto>(barRep));
        }

        /// <summary>
        /// Adds a BarRepresentative to the database.
        /// Authorization: Admin.
        /// </summary>
        /// <param name="barRepDto">
        /// is a BarRepresentativeDto object. Must match property attribute rules. 
        /// </param>
        /// <returns>
        /// Created (201) if BarRepresentative was added. <para></para>
        /// BadRequest (400) if model requirements weren't. Body will contain string: "Duplicate Key"
        /// if request failed because of duplicate key sql exception <para></para>
        /// 401 or 403 if authorization is unsuccessful. <para></para>
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(BarRepresentativeDto), StatusCodes.Status201Created)] 
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public IActionResult AddBarRepresentative([FromBody] BarRepresentativeDto barRepDto)
        {
            try
            {
                var barRep = _mapper.Map<BarRepresentative>(barRepDto);
                _unitOfWork.BarRepRepository.Add(barRep);
                _unitOfWork.Complete();
                return Created(string.Format($"api/BarRepresentative/{barRep.Username}"), barRepDto);
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
        /// Edits an BarRepresentative.
        /// Authorization: Admin, BarRepresentattive
        /// </summary>
        /// <param name="barRepDto">
        /// is a BarRepresentativeDto which holds edited data. <para></para>
        /// Must hold a Username which can be found in the database.  <para></para>
        /// Must match property attribute rules. 
        /// </param>
        /// <returns>
        /// Created (201) if edit was successful. <para></para>
        /// BadRequest (400) if edit was unsuccessful. See parameter requirements. <para></para>
        /// 401 or 403 if authorization is unsuccessful. <para></para>
        /// </returns>
        [HttpPut]
        [Authorize(Roles = "BarRep,Admin")]
        [ProducesResponseType(typeof(BarRepresentativeDto), StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
  
        public IActionResult EditBarRepresentative([FromBody] BarRepresentativeDto barRepDto)
        {
            try
            {
                var barRep = _mapper.Map<BarRepresentative>(barRepDto);
                _unitOfWork.BarRepRepository.Edit(barRep);
                _unitOfWork.Complete();
                return Created(string.Format($"api/BarRepresentative/{barRep.Username}"), barRepDto);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes an BarRepresentative from database. 
        /// Authorization: Admin
        /// </summary>
        /// <param name="username">
        /// is a string holding the username
        /// </param>
        /// <returns>
        /// Ok (200) if deletion was successful. <para></para>
        /// BadRequest (400) if deletion was unsuccessful. <para></para>
        /// 401 or 403 if authorization is unsuccessful. <para></para>
        /// </returns>
        [HttpDelete("{username}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK)]  
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteBarRepresentative(string username)
        {
            try
            {
                _unitOfWork.BarRepRepository.Delete(username);
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