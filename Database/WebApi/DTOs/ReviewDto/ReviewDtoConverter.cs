﻿using System.Collections.Generic;
using Database;

namespace WebApi.DTOs.ReviewDto
{
    public static class ReviewDtoConverter
    {
        public static ReviewDto ToDto(Review review)
        {
            var dto = new ReviewDto()
            {
                BarName = review.BarName,
                BarPressure = review.BarPressure,
                Username = review.Username,
            };
            return dto; 
        }

        public static Review ToReview(ReviewDto dto)
        {
            var review = new Review()
            {
                BarName = dto.BarName,
                Username = dto.Username,
                BarPressure = dto.BarPressure,
            };
            return review;
        }


    }
}