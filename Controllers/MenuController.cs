using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using retail_bff.Models;
using retail_bff.Services;
using Microsoft.AspNetCore.Authorization;
using retail_bff.Helpers;

namespace retail_bff.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {

        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService) {
            _menuService = menuService;
        }


        [HttpGet("{restaurantId}")]
        public ActionResult<List<MenuItem>> Get(Guid restaurantId)
        {
            try
            {
                var menu = _menuService.GetMenu(restaurantId);
                return Ok(new
                {
                    data = menu,
                    message = "Successfully fetched the whole menu"
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update menu items
        /// POST api/<MenuController>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateMenu([FromBody] List<MenuItem> menuItems)
        {
            try
            {
                var _orders = _menuService.UpdateMenu(menuItems);
                return Ok(new
                {
                    data = menuItems,
                    message = "Successfully updated menu items."
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
