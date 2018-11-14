using System.Collections.Generic;

namespace P02.One_To_Many_Relation
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Department Department { get; set; }

        public int? ManagerId { get; set; }

        public Employee Manager { get; set; }

        public List<Employee> ManagerEmployees { get; set; } = new List<Employee>();
    }
}
