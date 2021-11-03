using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using retail_bff.Helpers;
using retail_bff.Models;
using retail_bff.Services;
using System;
using System.Collections.Generic;

namespace retail_bff.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        /// <summary>
        /// Get orders by RestaurantId id
        /// </summary>
        /// <param name="restaurantId">RestaurantId id</param>
        /// <returns>Order entity</returns>
        // GET api/<OrderController>/5
        [HttpGet("{restaurantId}")]
        public ActionResult<Order> Get(Guid restaurantId)
        {
            try
            {
                var _coupon = _couponService.Get(restaurantId);
                return Ok(new
                {
                    data = _coupon,
                    message = "Successfully fetched Coupons."
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Create new coupon
        /// POST api/<CouponController>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create([FromBody] Coupon order)
        {
            try
            {
                var _order = _couponService.Create(order);
                return Ok(new
                {
                    data = order,
                    message = "Successfully created a coupon."
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Create new coupon
        /// POST api/<CouponController>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveCoupons([FromBody] List<Coupon> orders)
        {
            try
            {
                var _orders = _couponService.UpdateChangedCoupons(orders);
                return Ok(new
                {
                    data = orders,
                    message = "Successfully saved the coupons."
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Create new coupon
        /// POST api/<CouponController>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteCoupon([FromBody] Coupon coupon)
        {
            try
            {
                var _orders = _couponService.MakeInactive(coupon);
                return Ok(new
                {
                    data = coupon,
                    message = "Successfully deleted the coupon."
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
