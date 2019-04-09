﻿using System;
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
            var review = GetReview(username, BarName);
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
            // Pull review from database
            var review = GetReview(receivedReview.Username, receivedReview.BarName);
            // Change the user rating
            if (review != null)
            {
                review.BarPressure = receivedReview.BarPressure;
                // Save the changes
                _unitOfWork.UpdateBarRating(receivedReview.BarName);
                _unitOfWork.Complete();

                var returnReview = ReviewDtoConverter.ToDto(review);
                return Created(string.Format($"api/bars/{review.BarName}/reviews/{review.Username}"), returnReview);
            }
            else
                return BadRequest();
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

        private Review GetReview(string username, string barName)
        {
            // This is the key of a Review. 
            string[] key = new string[2];
            key[0] = barName;
            key[1] = username;
            var review = _unitOfWork.ReviewRepository.Get(key);
            return review;
        }

        
    }
}