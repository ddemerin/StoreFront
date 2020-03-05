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

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreItem>> GetOneItem(int id)
        {
            var item = await db.StoreItems.FirstOrDefaultAsync(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet("sku/{SKU}")]
        public async Task<ActionResult<StoreItem>> GetSKU(string SKU)
        {
            var item = await db.StoreItems.FirstOrDefaultAsync(i => i.SKU == SKU);
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

        [HttpPost]
        public async Task<ActionResult<StoreItem>> AddItem(StoreItem newItem)
        {
            await db.StoreItems.AddAsync(newItem);
            await db.SaveChangesAsync();
            return Ok(newItem);
        }

        [HttpPut]
        public async Task<ActionResult<StoreItem>> UpdateItem(int id, StoreItem newData)
        {
            newData.Id = id;
            db.Entry(newData).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok(newData);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOne(int id)
        {
            var item = await db.StoreItems.FirstOrDefaultAsync(i => i.Id == id);
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
