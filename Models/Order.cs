using System;
using System.Collections.Generic;

namespace StoreFront.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate{ get; set; }
        public string CustomerName { get; set; }

        public List<ItemOrder> ItemOrders { get; set; } = new List<ItemOrder>();
    }
}