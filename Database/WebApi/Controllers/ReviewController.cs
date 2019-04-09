using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.ReviewDto;

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
        /// Constructor for the controller. 
        /// </summary>
        /// <param name="UnitOfWork">
        /// UnitOfWork implementation is dependency injected through configuration in Startup.cs
        /// </param>
        public ReviewController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        /// <summary>
        /// Returns the average rating of a Bar. 
        /// </summary>
        /// <param name="BarName">
        /// is a string to identify the Bar by its BarName. 
        /// </param>
        /// <returns>
        /// Ok (200) and the average rating as a double if successful. <para></para>
        /// NotFound(404) if the rating was not found. 
        /// </returns>
        [HttpGet] 
        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public IActionResult GetAverageReviewScore([FromRoute] string BarName)
        {
            var bar = _unitOfWork.BarRepository.Get(BarName);
            if (bar != null)
                return Ok(bar.AvgRating);
            else
                return NotFound();
        }

        /// <summary>
        /// Returns the single review made by 1 user of 1 bar. 
        /// </summary>
        /// <param name="username">
        /// is a string holding the username of the user. 
        /// </param>
        /// <param name="BarName">
        /// is a string holding the bar name of the bar. 
        /// </param>
        /// <returns>
        /// Ok (200) and the review as an ReviewDto if found. <para></para>
        /// NotFound (404) if the review could not be found. <para></para>'
        /// The review can only be found if username and BarName matches a Review saved in the database. 
        /// </returns>
        [HttpGet("{username}")]
        [ProducesResponseType(typeof(ReviewDto), 200)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public IActionResult GetUserReview(string username, string BarName)
        {
            var review = _unitOfWork.ReviewRepository.Get(new object[] {BarName, username});
            if (review != null)
            {
                var reviewDto = ReviewDtoConverter.ToDto(review);
                return Ok(reviewDto);
            }
            else
                return NotFound();
        }

        /// <summary>
        /// Edits an user review. 
        /// </summary>
        /// <param name="receivedReview">
        /// is a ReviewDto. Must match property attribute rules.  
        /// </param>
        /// <returns>
        /// Created (201) if edit was successful. <para></para>
        /// BadRequest (400) if edit was unsuccessful. 
        /// </returns>
        [HttpPut]
        [ProducesResponseType(typeof(ReviewDto), 201)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        public IActionResult EditUserReview([FromBody]ReviewDto receivedReview)
        {
            try
            {
                var review = ReviewDtoConverter.ToReview(receivedReview);
                _unitOfWork.ReviewRepository.Edit(review);
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
        /// </summary>
        /// <param name="reviewDto">
        /// is a ReviewDto to be added. Must match property attribute rules. 
        /// </param>
        /// <returns>
        /// Created (201) if successful. <para></para>
        /// BadRequest (400) if unsuccessful. 
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(ReviewDto), 201)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public IActionResult AddUserReview([FromBody] ReviewDto reviewDto)
        {
            try
            {
                var review = ReviewDtoConverter.ToReview(reviewDto);
                _unitOfWork.ReviewRepository.Add(review);
                _unitOfWork.Complete();
                _unitOfWork.UpdateBarRating(reviewDto.BarName);
                _unitOfWork.Complete();
                return Created(string.Format($"api/bars/{review.BarName}/reviews/{review.Username}"), reviewDto);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

         
        /// <summary>
        /// Deletes a review
        /// </summary>
        /// <param name="BarName">
        /// is a string identifying the bar. 
        /// </param>
        /// <param name="username">
        /// is a string identifying the user.
        /// </param>
        /// <returns>
        /// Ok (200) if deletion was successful. <para></para>
        /// NotFound (404) if deletion was unsuccessful.<para></para>
        /// There must exist a Review with the provided
        /// BarName and username for deletion to be successful
        /// </returns>
        [HttpDelete("{username}")]
        [ProducesResponseType(typeof(Nullable), 200)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public IActionResult DeleteUserReview(string BarName, string username)
        {
            try
            {   //Create keys and delete. 
                _unitOfWork.ReviewRepository.Delete( new string[]{BarName, username});
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