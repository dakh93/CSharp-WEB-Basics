
using Microsoft.EntityFrameworkCore;
using P03.Many_To_Many_Relation.Models;
using P03.Many_To_Many_Relation.Data;

namespace P03.Many_To_Many_Relation.Data
{
    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<StudentsCourses> Courses { get; set; }

        public DbSet<StudentsCourses>  StudentsCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Student>()
                .HasMany(s => s.Courses)
                .WithOne(sc => sc.Student)
                .HasForeignKey(sc => sc.StudentId);

            builder
                .Entity<Course>()
                .HasMany(c => c.Students)
                .WithOne(sc => sc.Course)
                .HasForeignKey(sc => sc.CourseId);

            builder
                .Entity<StudentsCourses>()
                .HasKey(sc => new {sc.StudentId, sc.CourseId});


        }
    }
}
