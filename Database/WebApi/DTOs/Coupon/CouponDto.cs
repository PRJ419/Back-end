using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.Coupon
{
    /// <summary>
    /// Data transfer object class of Coupon class
    /// </summary>
    public class CouponDto
    {
        /// <summary>
        /// Property for getting and setting the bar name associated to the coupon
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string BarName { get; set; }

        /// <summary>
        /// Property for getting and setting the coupon id for a coupon
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string CouponID { get; set; }

        /// <summary>
        /// Property for getting and setting the expiration date of a coupon
        /// </summary>
        public DateTime ExpirationDate { get; set; }
    }
}