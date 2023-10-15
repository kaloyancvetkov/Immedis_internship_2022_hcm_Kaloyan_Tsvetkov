namespace HumanCapitalManagment.Infrastructure
{
    using HumanCapitalManagment.Data;
    using HumanCapitalManagment.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<HCMDbContext>();

            data.Database.Migrate();

            SeedDepartments(data);

            return app;
        }

        private static void SeedDepartments(HCMDbContext data)
        {
            if (data.Departments.Any())
            {
                return;
            }

            data.Departments.AddRange(new[]
            {
                new Department { Name = "Finance" },
                new Department { Name = "Sales" },
                new Department { Name = "Marketing" },
                new Department { Name = "Logistics" },
                new Department { Name = "Accounting" },
                new Department { Name = "Security" },
                new Department { Name = "Management" },
                new Department { Name = "Technology" },
                new Department { Name = "Customer Service" },
                new Department { Name = "Production" },
                new Department { Name = "Engineering" },
                new Department { Name = "Quality Assurance" },
            });

            data.SaveChanges();
        }
    }
}
