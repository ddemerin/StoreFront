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
    public class StoreItemController : ControllerBase
    {
        public DatabaseContext db { get; set; } = new DatabaseContext();

        [HttpGet("location/{locationid}")]
        public async Task<ActionResult<List<StoreItem>>> GetAllItemsInLoc(int locationId)
        {
            var allLocItems = await db.StoreItems.Where(i => i.LocationId == locationId).ToListAsync();
            if (allLocItems == null)
            {
                return NotFound();
            }
            return Ok(allLocItems);
        }

        [HttpGet("{id}/{locationid}")]
        public async Task<ActionResult<List<StoreItem>>> GetOneItemInLoc(int id, int locationId)
        {
            var oneLocItem = await db.StoreItems.FirstOrDefaultAsync(i => i.Id == id && i.LocationId == locationId);
            if (oneLocItem == null)
            {
                return NotFound();
            }
            return Ok(oneLocItem);
        }

        [HttpGet("sku/{SKU}")]
        public async Task<ActionResult<StoreItem>> GetSKU(string SKU)
        {
            var item = await db.StoreItems.Where(i => i.SKU == SKU).ToListAsync();
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet("outofstock")]
        public async Task<ActionResult<List<StoreItem>>> GetOutOfStock()
        {
            var outOfStock = await db.StoreItems.Where(i => i.NumberInStock == 0).ToListAsync();
            if (outOfStock == null)
            {
                return NotFound();
            }
            return Ok(outOfStock.ToList());
        }

        [HttpGet("outofstock/{locationId}")]
         public async Task<ActionResult<List<StoreItem>>> GetOutOfStockFromLoc(int locationId)
        {
            var outOfStock = await db.StoreItems.Where(i => i.NumberInStock == 0 && i.LocationId == locationId).ToListAsync();
            if (outOfStock == null)
            {
                return NotFound();
            }
            return Ok(outOfStock.ToList());
        }

        [HttpPost("{locationId}")]
        public async Task<ActionResult<StoreItem>> AddItemToLoc (int locationId, StoreItem newItem)
        {
            newItem.LocationId = locationId;
            await db.StoreItems.AddAsync(newItem);
            await db.SaveChangesAsync();
            return Ok(newItem);
        }

        [HttpPut("{id}/{locationid}")]
        public async Task<ActionResult<StoreItem>> UpdateItemInLoc (int id, int locationId, StoreItem newData)
        {
            newData.Id = id;
            newData.LocationId = locationId;
            db.Entry(newData).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok(newData);
        }

        [HttpDelete("{id}/{locationId}")]
        public async Task<ActionResult> DeleteOne(int id, int locationId)
        {
            var item = await db.StoreItems.FirstOrDefaultAsync(i => i.Id == id && i.LocationId == locationId);
            if (item == null)
            {
                return NotFound();
            }
            db.StoreItems.Remove(item);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}