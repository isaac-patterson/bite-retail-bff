using Microsoft.EntityFrameworkCore;
using retail_bff.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace retail_bff.Services
{
    public interface IMenuService
    {
        List<MenuItem> GetMenu(Guid restaurantId);
        List<MenuItem> UpdateMenu(List<MenuItem> menuItems);
    }

    public class MenuService : IMenuService
    {
        private DBContext _context;

        public MenuService(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// method using by API endpoint: /api/menu/get
        /// Use: Get list of menu items
        /// Input: Restaurant ID (Cognito ID?)
        /// </summary>
        /// <returns>Object</returns>
        public List<MenuItem> GetMenu(Guid restaurantId)
        {
            // return all pending orders
            return _context.MenuItem
                .Include(x => x.MenuItemOption)
                .ThenInclude(y => y.MenuItemOptionValue)
                .Where(m => m.RestaurantId == restaurantId)
                .ToList();
        }

        /// <summary>
        /// method using by API endpoint: /api/menu/update
        /// Use: Update retaurant menu items
        /// Input: List of menu items
        /// </summary>
        /// <returns>Object</returns>
        public List<MenuItem> UpdateMenu(List<MenuItem> menuItems)
        {
            _context.UpdateRange(menuItems);
            _context.SaveChanges();

            return menuItems;
        }
    }
}
