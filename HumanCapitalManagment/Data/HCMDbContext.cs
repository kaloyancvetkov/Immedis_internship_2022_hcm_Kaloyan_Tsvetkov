namespace HumanCapitalManagment.Data
{
    using HumanCapitalManagment.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class HCMDbContext : IdentityDbContext<User>
    {
        public HCMDbContext(DbContextOptions<HCMDbContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; init; }

        public DbSet<Department> Departments { get; init; }

        public DbSet<HRSpecialist> HRSpecialists { get; init; }

        public DbSet<Candidate> Candidates { get; init; }

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
                .HasOne(e => e.HRSpecialist)
                .WithMany(h => h.Employees)
                .HasForeignKey(e => e.HRSpecialistId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<HRSpecialist>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<HRSpecialist>(h => h.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Candidate>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Candidates)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Employee>()
                .Property(e => e.SalaryAmount)
                .HasColumnType("decimal(10,2)")
                .IsRequired(true);

            base.OnModelCreating(builder);
        }
    }
}
