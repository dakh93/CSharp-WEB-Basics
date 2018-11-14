using System.Collections.Generic;

namespace P02.One_To_Many_Relation
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
