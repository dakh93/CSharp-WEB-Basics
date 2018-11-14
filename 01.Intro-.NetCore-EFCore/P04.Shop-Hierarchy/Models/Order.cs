
using System.Collections.Generic;

namespace P04.Shop_Hierarchy.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<ItemOrder> Items { get; set; } = new List<ItemOrder>();

    }
}
