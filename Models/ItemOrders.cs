using System;
using System.Collections.Generic;

namespace StoreFront.Models
{
    public class ItemOrder
    {
        public int Id { get; set; }
        public int? StoreItemId { get; set; }
        public StoreItem StoreItem { get; set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; }
    }
}