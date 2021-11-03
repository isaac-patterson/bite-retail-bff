using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using retail_bff.Helpers;
using retail_bff.Models;

namespace retail_bff.Services
{
    public interface ICouponService
    {
        /// <summary>
        /// Use: Get coupon by Id
        /// </summary>
        /// <param name="couponCode">coupon code</param>
        /// <returns>Object</returns>
        List<Coupon> Get(Guid restaurantId);
        Coupon Create(Coupon coupon);
        List<Coupon> UpdateChangedCoupons(List<Coupon> coupons);
        Coupon MakeInactive(Coupon coupon);
    }

    public class CouponService : ICouponService
    {
        private DBContext _context;
        public CouponService(DBContext context)
        {
            _context = context;
        }

        ///<inheritdoc/>
        public List<Coupon> Get(Guid restaurantId)
        {
            try
            {
                List<Coupon> coupons = _context.Coupon
                    .Where(x => x.RestaurantId == restaurantId && x.ExpiryDate > DateTime.UtcNow && x.UserDeleted == false)
                    .OrderByDescending(x => x.ExpiryDate)
                    .ToList();

                return coupons;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        /// <summary>
        /// method using by API endpoint: /api/order/create
        /// Use: create new pending order 
        /// Input: coupon information
        /// </summary>
        /// <returns>Object</returns>
        public Coupon Create(Coupon coupon)
        {
            _context.Coupon.Add(coupon);
            _context.SaveChanges();

            return coupon;
        }

        /// <summary>
        /// method using by API endpoint: /api/order/create
        /// Use: updates all restaurants coupons
        /// Input: list of coupons
        /// </summary>
        /// <returns>Object</returns>
        public List<Coupon> UpdateChangedCoupons(List<Coupon> coupons)
        {
            _context.UpdateRange(coupons);
            _context.SaveChanges();

            return coupons;
        }

        /// <summary>
        /// method using by API endpoint: /api/order/create
        /// Use: used to effectively delete a coupon
        /// Input: list of coupons
        /// </summary>
        /// <returns>Object</returns>
        public Coupon MakeInactive(Coupon coupon)
        {
            coupon.UserDeleted = true;

            _context.Update(coupon);
            _context.SaveChanges();

            return coupon;
        }
    }
}
