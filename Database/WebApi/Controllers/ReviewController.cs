using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http;
using AutoMapper;
using Database;
using Database.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using WebApi.DTOs.ReviewDto;
using WebApi.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
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


namespace WebApi.Controllers
{
    /// <summary>
    /// ReviewController for the Web Api. <para></para>
    /// Returns ReviewDto objects. 
    /// </summary>
    
    [Route("api/bars/{BarName}/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        /// <summary>
        /// Reference to a UnitOfWork implementation. <para></para>
        /// Used to access database. 
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Field to store IMapper implementation.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the controller. 
        /// </summary>
        /// <param name="UnitOfWork">
        /// UnitOfWork implementation is dependency injected through configuration in Startup.cs
        /// </param>
        /// <param name="mapper">
        /// IMapper implementation used to map Dto object to model objects and vice versa. 
        /// </param>
        public ReviewController(IUnitOfWork UnitOfWork, IMapper mapper)
        {
            _unitOfWork = UnitOfWork;
            _mapper = mapper; 
        }

        /// <summary>
        /// Returns all the reviews of a bar.
        /// Authorization: Admin, Kunde, BarRepresentativ
        /// </summary>
        /// <param name="BarName">
        /// is a string to identify the Bar by its BarName. 
        /// </param>
        /// <returns>
        /// Ok (200) and a List&lt;ReviewDto&gt;  <para></para>
        /// Unauthorized (401) if authentication is unsuccessful. <para></para>
        /// NotFound(404) if no reviews were found.
        /// </returns>
        [HttpGet] 
        [Authorize(Roles = "Kunde,Admin,BarRep")]
        [ProducesResponseType(typeof(List<ReviewDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public IActionResult GetReviews([FromRoute] string BarName)
        {
            var reviews = _unitOfWork.ReviewRepository.Find(x => x.BarName == BarName);
            if(reviews.Any())
            {
                var reviewDtoList = Converter.GenericListConvert<Review, ReviewDto>(reviews, _mapper);
                return Ok(reviewDtoList);
            }
            else
                return NotFound();
        }

        /// <summary>
        /// Returns the single review made by 1 user of 1 bar. 
        /// Authorization: Admin, Kunde
        /// </summary>
        /// <param name="username">
        /// is a string holding the username of the user. 
        /// </param>
        /// <param name="BarName">
        /// is a string holding the bar name of the bar. 
        /// </param>
        /// <returns>
        /// Ok (200) and the review as an ReviewDto if found. <para></para>
        /// Unauthorized (401) if authentication is unsuccessful. <para></para>
        /// NotFound (404) if the review could not be found. <para></para>'
        /// The review can only be found if username and BarName matches a Review saved in the database. 
        /// </returns>
        [HttpGet("{username}")]
        [Authorize(Roles = "Kunde,Admin")]
        [ProducesResponseType(typeof(ReviewDto), 200)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public IActionResult GetUserReview(string username, string BarName)
        {
            var review = _unitOfWork.ReviewRepository.Get(new object[] {BarName, username});
            if (review != null)
            {
                var reviewDto = _mapper.Map<ReviewDto>(review);
                return Ok(reviewDto);
            }
            else
                return NotFound();
        }

        /// <summary>
        /// Edits an user review. 
        /// Authorization: Admin, Kunde
        /// </summary>
        /// <param name="receivedReview">
        /// is a ReviewDto. Must match property attribute rules.  
        /// </param>
        /// <returns>
        /// Created (201) if edit was successful. <para></para>
        /// Unauthorized (401) if authentication is unsuccessful. <para></para>
        /// BadRequest (400) if edit was unsuccessful. Body will contain string: "Duplicate Key"
        /// if request failed because of duplicate key sql exception 
        /// </returns>
        [HttpPut]
        [Authorize(Roles = "Kunde,Admin")]
        [ProducesResponseType(typeof(ReviewDto), 201)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        public IActionResult EditUserReview([Microsoft.AspNetCore.Mvc.FromBody]ReviewDto receivedReview)
        {
            try
            {
                var review = _mapper.Map<Review>(receivedReview);
                _unitOfWork.ReviewRepository.Edit(review);
                _unitOfWork.Complete();
                _unitOfWork.UpdateBarRating(review.BarName);
                _unitOfWork.Complete();
                return Created(string.Format($"api/bars/{review.BarName}/reviews/{review.Username}"), receivedReview);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Adds a review.
        /// Authorization: Admin, Kunde
        /// </summary>
        /// <param name="reviewDto">
        /// is a ReviewDto to be added. Must match property attribute rules. 
        /// </param>
        /// <returns>
        /// Created (201) if successful. <para></para>
        /// Unauthorized (401) if authentication is unsuccessful. <para></para>
        /// BadRequest (400) if unsuccessful. 
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "Kunde,Admin")]
        [ProducesResponseType(typeof(ReviewDto), 201)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        public IActionResult AddUserReview([Microsoft.AspNetCore.Mvc.FromBody] ReviewDto reviewDto)
        {
            try
            {
                var review = _mapper.Map<Review>(reviewDto);
                _unitOfWork.ReviewRepository.Add(review);
                _unitOfWork.Complete();
                _unitOfWork.UpdateBarRating(reviewDto.BarName);
                _unitOfWork.Complete();
                return Created(string.Format($"api/bars/{review.BarName}/reviews/{review.Username}"), reviewDto);
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
        /// Deletes a review.
        /// Authorization: Admin
        /// </summary>
        /// <param name="BarName">
        /// is a string identifying the bar. 
        /// </param>
        /// <param name="username">
        /// is a string identifying the user.
        /// </param>
        /// <returns>
        /// Ok (200) if deletion was successful. <para></para>
        /// Unauthorized (401) if authentication is unsuccessful. <para></para>
        /// NotFound (404) if deletion was unsuccessful.<para></para>
        /// There must exist a Review with the provided
        /// BarName and username for deletion to be successful
        /// </returns>
        [HttpDelete("{username}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Nullable), 200)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        public IActionResult DeleteUserReview(string BarName, string username)
        {
            try
            {   //Create keys and delete. 
                _unitOfWork.ReviewRepository.Delete( new object[]{BarName, username});
                _unitOfWork.Complete();
                _unitOfWork.UpdateBarRating(BarName);
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