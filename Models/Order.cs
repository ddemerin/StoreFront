using System;
using System.Collections.Generic;

namespace StoreFront.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string ManagerName { get; set; }
        public string PhoneNumber { get; set; }

        public List<ItemOrder> ItemOrders { get; set; } = new List<ItemOrder>();
    }
}