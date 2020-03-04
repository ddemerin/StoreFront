using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreFront.Models;

namespace StoreFront.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreItemController : ControllerBase
    {
        public DatabaseContext db { get; set; } = new DatabaseContext();
    }
}