using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using retail_bff.Helpers;
using retail_bff.Models;

namespace retail_bff.Services
{
    public interface IRestaurantService
    {
        /// <summary>
        /// Use: Get restaurant by Id
        /// </summary>
        /// <param name="restaurantId">restaurant code</param>
        /// <returns>Object</returns>
        Restaurant GetById(Guid restaurantId);

        /// <summary>
        /// Use: Update a restaurants details
        /// </summary>
        /// <param name="restaurant">restaurant details</param>
        /// <returns>Object</returns>
        Restaurant UpdateRestaurantDetails(Restaurant restaurant);
    }

    public class RestaurantService : IRestaurantService
    {
        private DBContext _context;
        public RestaurantService(DBContext context)
        {
            _context = context;
        }

        ///<inheritdoc/>
        public Restaurant GetById(Guid restaurantId)
        {
            try
            {
                Restaurant restaurant = _context.Restaurant
                    .Include(x => x.RestaurantOpenDays)
                    .Where(m => m.RestaurantId == restaurantId)
                    .FirstOrDefault()
                    ;

                if (restaurant == null)
                {
                    throw new AppException("Restaurant is not valid.");
                }

                return restaurant;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public Restaurant UpdateRestaurantDetails(Restaurant restaurant)
        {
            _context.Update(restaurant);
            _context.SaveChanges();

            return restaurant;
        }
    }
}
