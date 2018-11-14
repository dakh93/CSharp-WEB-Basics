using System.Collections.Generic;
using System.Text;

namespace P04.Shop_Hierarchy.Models
{
    public class Salesman
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Customer> Customers { get; set; } = new List<Customer>();
    }
}
