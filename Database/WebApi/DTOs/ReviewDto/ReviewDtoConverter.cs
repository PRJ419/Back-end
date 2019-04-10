using System.Collections.Generic;
using Database;

namespace WebApi.DTOs.ReviewDto
{
    /// <summary>
    /// Converter class for Review and ReviewDto
    /// </summary>
    public static class ReviewDtoConverter
    {
        /// <summary>
        /// Converts a Review to a ReviewDto
        /// </summary>
        /// <param name="review">
        /// is a Review object 
        /// </param>
        /// <returns>
        /// is the ReviewDto equivalent of the parameter. 
        /// </returns>
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

        /// <summary>
        /// Converts from ReviewDto to Review. 
        /// </summary>
        /// <param name="dto">
        /// is a ReviewDto
        /// </param>
        /// <returns>
        /// Returns the Review equivalent to the parameter. 
        /// </returns>
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

        /// <summary>
        /// Converts from a IEnumerable of Review objects to an equivalent List of ReviewDto objects
        /// </summary>
        /// <param name="reviews">
        /// is a IEnumerable&lt;Review&gt;
        /// </param>
        /// <returns>
        /// Returns the List&lt;ReviewDto&gt; equivalent of the parameter. 
        /// </returns>
        public static List<ReviewDto> ToDtoList(IEnumerable<Review> reviews)
        {
            var dtoList =  new List<ReviewDto>();
            foreach (var review in reviews)
            {
                dtoList.Add(ToDto(review));
            }

            return dtoList;
        }


    }
}