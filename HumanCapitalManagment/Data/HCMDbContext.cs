namespace HumanCapitalManagment.Data
{
    using HumanCapitalManagment.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class HCMDbContext : IdentityDbContext
    {
        public HCMDbContext(DbContextOptions<HCMDbContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; init; }

        public DbSet<Department> Departments { get; init; }

        public DbSet<Salary> Salaries { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Employee>()
                .HasOne(e => e.Salary)
                .WithOne(s => s.Employee)
                .HasForeignKey<Salary>(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
