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

        [HttpGet]
        public async Task<ActionResult<List<StoreItem>>> GetAllItems()
        {
            var allItems = await db.StoreItems.OrderBy(i => i.Id).ToListAsync();
            if (allItems == null)
            {
                return NotFound();
            }
            return Ok(allItems);
        }

        [HttpGet("location/{locationid}")]
        public async Task<ActionResult<List<StoreItem>>> GetAllItemsInLoc(int locationId)
        {
            var allLocItems = db.StoreItems.Where(i => i.LocationId == locationId);
            if (allLocItems == null)
            {
                return NotFound();
            }
            return Ok(await allLocItems.ToListAsync());
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
            var item = db.StoreItems.Where(i => i.SKU == SKU);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(await item.ToListAsync());
        }

        [HttpGet("outofstock")]
        public async Task<ActionResult<List<StoreItem>>> GetOutOfStock()
        {
            var outOfStock = db.StoreItems.Where(i => i.NumberInStock == 0);
            if (outOfStock == null)
            {
                return NotFound();
            }
            return Ok(await outOfStock.ToListAsync());
        }

        [HttpGet("outofstock/{locationId}")]
         public async Task<ActionResult<List<StoreItem>>> GetOutOfStockFromLoc(int locationId)
        {
            var outOfStock = db.StoreItems.Where(i => i.NumberInStock == 0 && i.LocationId == locationId);
            if (outOfStock == null)
            {
                return NotFound();
            }
            return Ok(await outOfStock.ToListAsync());
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