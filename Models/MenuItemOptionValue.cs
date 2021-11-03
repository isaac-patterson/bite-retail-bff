using System;

namespace retail_bff.Models
{
    public class MenuItemOptionValue
    {
        public int MenuItemOptionValueId { get; set; }
        public int MenuItemOptionId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}