namespace HumanCapitalManagment.Services.Employees
{
    using HumanCapitalManagment.Data;
    using HumanCapitalManagment.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class EmployeeService : IEmployeeService
    {
        private readonly HCMDbContext data;

        public EmployeeService(HCMDbContext data)
            => this.data = data;

        public EmployeeQueryServiceModel All(
            string department,
            string searchTerm,
            EmployeesSorting sorting,
            int currentPage,
            int employeesPerPage)
        {
            var employeesQuery = this.data.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(department))
            {
                employeesQuery = employeesQuery.Where(e => e.Department.Name == department);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                employeesQuery = employeesQuery.Where(e =>
                    e.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    e.EmailAddress.ToLower().Contains(searchTerm.ToLower()) ||
                    e.PhoneNumber.Contains(searchTerm) ||
                    e.Nationality.ToLower().Contains(searchTerm.ToLower()) ||
                    e.DateOfBirth.ToString().Contains(searchTerm) ||
                    e.SalaryStatus.ToLower().Contains(searchTerm.ToLower()) ||
                    e.Gender.ToLower().Contains(searchTerm.ToLower()));
            }

            employeesQuery = sorting switch
            {
                EmployeesSorting.Name => employeesQuery.OrderBy(e => e.Name),
                EmployeesSorting.Department => employeesQuery.OrderBy(e => e.Department.Name),
                EmployeesSorting.Nationality => employeesQuery.OrderBy(e => e.Nationality),
                EmployeesSorting.DateOfBirth => employeesQuery.OrderBy(e => e.DateOfBirth),
                EmployeesSorting.SalaryAmount => employeesQuery.OrderByDescending(e => e.SalaryAmount),
                EmployeesSorting.DateAdded or _ => employeesQuery.OrderBy(e => e.Id)
            };

            var totalEmployees = employeesQuery.Count();

            var employees = employeesQuery
                .Skip((currentPage - 1) * employeesPerPage)
                .Take(employeesPerPage)
                .Select(e => new EmployeeServiceModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    EmailAddress = e.EmailAddress,
                    PhoneNumber = e.PhoneNumber,
                    Nationality = e.Nationality,
                    DateOfBirth = e.DateOfBirth.ToShortDateString(),
                    Gender = e.Gender,
                    Department = e.Department.Name,
                    SalaryAmount = e.SalaryAmount,
                    SalaryStatus = e.SalaryStatus,
                })
                .ToList();

            return new EmployeeQueryServiceModel
            {
                TotalEmployees = totalEmployees,
                CurrentPage = currentPage,
                EmployeesPerPage = employeesPerPage,
                Employees = employees
            };
        }

        public IEnumerable<string> AllDepartments()
            => this.data
                .Departments
                .Select(d => d.Name)
                .OrderBy(d => d)
                .ToList();
    }
}
