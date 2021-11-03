using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using retail_bff.Services;
using retail_bff.Models;
using retail_bff.Helpers;

namespace retail_bff.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService) {
            _restaurantService = restaurantService;
        }

        [HttpGet("{restaurantId}")]
        public ActionResult<Restaurant> GetById(Guid restaurantId)
        {
            try
            {
                var restro = _restaurantService.GetById(restaurantId);

                return Ok(new
                {
                    data = restro,
                    message = "Successfully returned restaurant details."
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult<Restaurant> Update([FromBody] Restaurant restaurant)
        {
            try
            {
                var restro = _restaurantService.UpdateRestaurantDetails(restaurant);

                return Ok(new
                {
                    data = restro,
                    message = "Successfully returned restaurant details."
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
