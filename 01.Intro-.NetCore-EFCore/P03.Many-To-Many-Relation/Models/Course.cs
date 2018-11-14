
using System.Collections.Generic;

namespace P03.Many_To_Many_Relation.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<StudentsCourses> Students { get; set; } = new List<StudentsCourses>();
    }
}
