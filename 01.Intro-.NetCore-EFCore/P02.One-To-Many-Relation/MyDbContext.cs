using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;

namespace P02.One_To_Many_Relation
{
    public class MyDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                
                builder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=EmployeesTest;Integrated Security=true");
            }
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Employee>()
                .HasKey(e => e.Id);

            builder.Entity<Employee>()
                .Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired(true);

            builder
                .Entity<Department>()
                .HasKey(e => e.Id);

            builder.Entity<Department>()
                .Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired(true);


            builder
                .Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(d => d.Id);

            builder.Entity<Employee>()
                .HasOne(e => e.Manager)
                .WithMany(m => m.ManagerEmployees)
                .HasForeignKey(e => e.ManagerId);
        }
    }
}
