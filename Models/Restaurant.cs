using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace retail_bff.Models
{
    public class Restaurant
    {
        public Guid RestaurantId { get; set; }

        [StringLength(50, MinimumLength = 6, ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string Name { get; set; }

        [StringLength(150, MinimumLength = 6, ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string Description { get; set; }

        public string CountryCode { get; set; }

        [StringLength(200, MinimumLength = 6, ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string Address { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(30, MinimumLength = 4, ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string Category { get; set; }
        
        [StringLength(50, MinimumLength = 6, ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string LogoIcon { get; set; }

        [Range(0, 1, ErrorMessage = "{0} must be a decimal between {2} and {1}.")]
        public decimal? Offer { get; set; }
        public List<RestaurantOpenDays> RestaurantOpenDays { get; set; }
    }
}