using System;
using System.ComponentModel.DataAnnotations;

namespace retail_bff.Models
{
    public class Coupon
    {
        public int? CouponId { get; set; }
        public Guid RestaurantId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        [Range(0, 1, ErrorMessage = "{0} must be a decimal between {2} and {1}.")]
        public decimal? Discount { get; set; }
        
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string DiscountCode { get; set; }
        public bool UserDeleted { get; set; }
    }
}
