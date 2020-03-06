using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFront.Models;

namespace StoreFront.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        public DatabaseContext db { get; set; } = new DatabaseContext();
        
        [HttpPost]
        public async Task<ActionResult<Location>> AddLoc(Location newLocation)
        {
            await db.Locations.AddAsync(newLocation);
            await db.SaveChangesAsync();
            return Ok(newLocation);
        }

        [HttpGet]
        public async Task<ActionResult<List<Location>>> GetAllLocation()
        {
            var allLocs = db.Locations.OrderBy(i => i.Id);
            if (allLocs == null)
            {
                return NotFound();
            }
            return Ok(await allLocs.ToListAsync());
        }
    }
}