using System;
using System.Collections.Generic;

namespace retail_bff.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid RestaurantId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public short? AvailableOptionsCount { get; set; }
        public List<MenuItemOption> MenuItemOption { get; set; }
    }
}