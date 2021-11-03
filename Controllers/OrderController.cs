using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using retail_bff.Helpers;
using retail_bff.Models;
using retail_bff.Services;
using System;

namespace retail_bff.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get pending orders
        /// </summary>
        /// <returns>Order entity</returns>
        // GET api/<OrderController>
        [HttpGet("{restaurantId}")]
        public ActionResult<Order> GetPending(Guid restaurantId)
        {
            try
            {
                var order = _orderService.GetAllPending(restaurantId);
                return Ok(new
                {
                    data = order,
                    message = "Successfully returned pending orders detail."
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get orders by RestaurantId id
        /// </summary>
        /// <param name="restaurantId">RestaurantId id</param>
        /// <returns>Order entity</returns>
        // GET api/<OrderController>/5
        [HttpGet("{restaurantId}")]
        public ActionResult<Order> GetConfirmed(Guid restaurantId)
        {
            try
            {
                var order = _orderService.GetAllConfirmed(restaurantId);
                return Ok(new
                {
                    data = order,
                    message = "Successfully returned orders detail."
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get orders by RestaurantId id
        /// </summary>
        /// <param name="restaurantId">RestaurantId id</param>
        /// <returns>Order entity</returns>
        // GET api/<OrderController>/5
        [HttpGet("{restaurantId}")]
        public ActionResult<Order> GetPickedUp(Guid restaurantId)
        {
            try
            {
                var order = _orderService.GetAllPickedUp(restaurantId);
                return Ok(new
                {
                    data = order,
                    message = "Successfully returned orders detail."
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update order status
        /// </summary>
        /// <param name="status">status</param>
        /// <returns></returns>
        [HttpPatch("{id}/{pickupDate}")]
        public ActionResult OrderStatusConfirmed(int id, DateTime pickupDate)
        {
            try
            {
                var _order = _orderService.ChangeStatusToConfirmed(id, pickupDate);
                return Ok(new
                {
                    data = _order,
                    message = "Successfully updated order."
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update order status
        /// </summary>
        /// <param name="status">status</param>
        /// <returns></returns>
        [HttpPatch("{id}/{pickedupDate}")]
        public ActionResult OrderStatusPickedUp(int id, DateTime pickedupDate)
        {
            try
            {
                var _order = _orderService.ChangeStatusToPickedUp(id, pickedupDate);
                return Ok(new
                {
                    data = _order,
                    message = "Successfully updated order."
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
