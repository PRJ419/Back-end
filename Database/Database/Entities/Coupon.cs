using System;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace Database
{
    public class Coupon
    {
        [MaxLength(150)]
        public string BarName { get; set; }

        [MaxLength(50)]
        public string CouponID { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Bar Bar { get; set; }
    }
}