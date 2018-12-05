
using System;

namespace StudentSystem.Data
{
    public class Homework
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public FileType ContentType { get; set; }

        public DateTime SubmissionDate { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }
    }
}
