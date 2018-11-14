using System.Collections.Generic;

namespace P03.Many_To_Many_Relation.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<StudentsCourses> Courses { get; set; } = new List<StudentsCourses>();
    }
}
