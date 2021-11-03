using Microsoft.EntityFrameworkCore;
using retail_bff.Helpers;
using retail_bff.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace retail_bff.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllPending(Guid restaurantId);
        IEnumerable<Order> GetAllConfirmed(Guid restaurantId);
        IEnumerable<Order> GetAllPickedUp(Guid restaurantId);

        Order ChangeStatusToConfirmed(int id, DateTime pickupDate);

        Order ChangeStatusToPickedUp(int id, DateTime pickedupDate);
    }

    public class OrderService : IOrderService
    {
        private DBContext _context;

        public OrderService(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Use: Get pending orders by restaurant ID
        /// </summary>
        /// <param name="restaurantId">restaurantId</param>
        /// <returns>Object</returns>
        public IEnumerable<Order> GetAllPending(Guid restaurantId)
        {
            // return all pending orders
            return _context.Orders
                .Where(x => x.RestaurantId == restaurantId)
                .Where(x => x.Status == "Pending")
                .Include(x => x.OrderItem)
                .ThenInclude(x => x.OrderItemOption)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();
        }

        /// <summary>
        /// Use: Get complete orders by  RestaurantId
        /// </summary>
        /// <param name="restaurantId">restaurantId</param>
        /// <returns>Object</returns>
        public IEnumerable<Order> GetAllConfirmed(Guid restaurantId)
        {
            // return user info based on ID
            return _context.Orders
                .Where(x => x.RestaurantId == restaurantId)
                .Where(x => x.Status == "Confirmed")
                .Include(x => x.OrderItem)
                .ThenInclude(x => x.OrderItemOption)
                .AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// Use: Get all pickedup RestaurantId
        /// </summary>
        /// <param name="restaurantId">restaurantId</param>
        /// <returns>Object</returns>
        public IEnumerable<Order> GetAllPickedUp(Guid restaurantId)
        {
            // return user info based on ID
            return _context.Orders
                .Where(x => x.RestaurantId == restaurantId)
                .Where(x => x.Status == "PickedUp")
                .Include(x => x.OrderItem)
                .ThenInclude(x => x.OrderItemOption)
                .AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// method using by API endpoint: /api/order/update/{id}
        /// Use: update order information or status
        /// Input: order information
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <param name="pickupDate">pickupDate</param>
        /// <returns>Object</returns>
        public Order ChangeStatusToConfirmed(int id, DateTime pickupDate)
        {
            var order = _context.Orders.FirstOrDefault(x => x.OrderId==id);

            if (order == null)
                throw new AppException("Order not found.");

            _context.Entry(order).State = EntityState.Detached;

            order.Status = "Confirmed";
            order.PickupDate = pickupDate;

            // update Order information
            _context.Orders.Update(order);
            _context.SaveChanges();

            _context.Dispose();
            return order;
        }

        /// <summary>
        /// method using by API endpoint: /api/order/update/{id}
        /// Use: update order information or status
        /// Input: order information
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <param name="pickedupDate">pickupDate</param>
        /// <returns>Object</returns>
        public Order ChangeStatusToPickedUp(int id, DateTime pickedupDate)
        {
            var order = _context.Orders.FirstOrDefault(x => x.OrderId == id);

            if (order == null)
                throw new AppException("Order not found.");

            _context.Entry(order).State = EntityState.Detached;

            order.Status = "PickedUp";
            order.PickedupDate = pickedupDate;

            // update Order information
            _context.Orders.Update(order);
            _context.SaveChanges();

            _context.Dispose();
            return order;
        }
    }
}
