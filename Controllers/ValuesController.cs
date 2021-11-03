using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace retail_bff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // Used as health check from aws ec2
        [HttpGet]
        public ActionResult<string> Get()
        {
            Console.WriteLine("Values Controller!");
            return "healthy";
        }
    }
}

