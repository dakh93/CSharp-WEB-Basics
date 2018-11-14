
using System.Collections.Generic;

namespace P04.Shop_Hierarchy.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public List<ItemOrder> Orders { get; set; } = new List<ItemOrder>();

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
