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
    [Route("api/bars/{BarName}/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public ReviewController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

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

        [HttpGet("{username}")] // TODO: NOGET SMUKKESERING!
        public IActionResult GetUserReview(string username, string BarName)
        {
            var review = _unitOfWork.ReviewRepository.Get(new[] {BarName, username});
            if (review != null)
            {
                var reviewDto = ReviewDtoConverter.ToDto(review);
                return Ok(reviewDto);
            }
            else
                return NotFound();
        }

        [HttpPut]
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

        [HttpPost]
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

        [HttpDelete("{username}")]
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