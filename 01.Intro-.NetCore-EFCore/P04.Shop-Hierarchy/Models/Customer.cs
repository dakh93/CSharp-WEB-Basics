using System.Collections.Generic;

namespace P04.Shop_Hierarchy.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Salesman Salesman { get; set; }

        public int SalesmanId { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
