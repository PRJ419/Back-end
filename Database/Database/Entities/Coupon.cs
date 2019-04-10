using System;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace Database
{
    public class Coupon
    {
        /// <summary>
        /// Property for getting and setting the bar name associated to the coupon
        /// </summary>
        [MaxLength(150)]
        public string BarName { get; set; }

        /// <summary>
        /// Property for getting and setting the coupon id for a coupon
        /// </summary>
        [MaxLength(50)]
        public string CouponID { get; set; }

        /// <summary>
        /// Property for getting and setting the expiration date of a coupon
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Navigational property required by the database for finding the bar associated to the coupon
        /// </summary>
        public virtual Bar Bar { get; set; }
    }
}