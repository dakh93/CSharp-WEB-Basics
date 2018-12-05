using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Data
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ResourceType ResourceType { get; set; }

        public string Url { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public List<License> Licenses { get; set; } = new List<License>();
    }
}
