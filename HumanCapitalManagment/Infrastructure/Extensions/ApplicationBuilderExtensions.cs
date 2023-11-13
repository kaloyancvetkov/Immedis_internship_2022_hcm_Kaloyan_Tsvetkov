namespace HumanCapitalManagment.Infrastructure.Extensions
{
    using HumanCapitalManagment.Data;
    using HumanCapitalManagment.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static Areas.Admin.AdminConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);
            SeedDepartments(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetService<HCMDbContext>();

            data.Database.Migrate();
        }

        private static void SeedDepartments(IServiceProvider services)
        {
            var data = services.GetService<HCMDbContext>();

            if (data.Departments.Any())
            {
                return;
            }

            data.Departments.AddRange(new[]
            {
                new Department { Name = "Accounting" },
                new Department { Name = "Customer Service" },
                new Department { Name = "Engineering" },
                new Department { Name = "Finance" },
                new Department { Name = "Logistics" },
                new Department { Name = "Management" },
                new Department { Name = "Marketing" },
                new Department { Name = "Production" },
                new Department { Name = "Quality Assurance" },
                new Department { Name = "Sales" },
                new Department { Name = "Security" },
                new Department { Name = "Technology" },
            });

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                {
                    return;
                }

                var role = new IdentityRole { Name = AdministratorRoleName };

                await roleManager.CreateAsync(role);

                const string adminEmail = "admin@hcm.com";
                const string adminPassword = "admin123";

                var user = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FullName = "Admin"
                };

                await userManager.CreateAsync(user, adminPassword);

                await userManager.AddToRoleAsync(user, role.Name);
            })
            .GetAwaiter()
            .GetResult();
        }
    }
}
