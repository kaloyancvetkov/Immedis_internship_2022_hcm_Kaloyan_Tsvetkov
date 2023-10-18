namespace HumanCapitalManagment.Controllers
{
    using HumanCapitalManagment.Data;
    using HumanCapitalManagment.Data.Models;
    using HumanCapitalManagment.Models.Employees;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class EmployeesController : Controller
    {
        private readonly HCMDbContext data;

        public EmployeesController(HCMDbContext data)
        {
            this.data = data;
        }

        public IActionResult Add() => View(new AddEmployeeFormModel
        {
            Departments = this.GetDepartments()
        });

        [HttpPost]
        public IActionResult Add(AddEmployeeFormModel employee)
        {
            if (!this.data.Departments.Any(d => d.Id == employee.DepartmentId))
            {
                this.ModelState.AddModelError(nameof(employee.DepartmentId), "Department doesn't exist.");
            }

            if (!ModelState.IsValid)
            {
                employee.Departments = this.GetDepartments();

                return View(employee);
            }

            var employeeData = new Employee
            {
                Name = employee.Name,
                EmailAddress = employee.EmailAddress,
                PhoneNumber = employee.PhoneNumber,
                Nationality = employee.Nationality,
                DateOfBirth = employee.DateOfBirth,
                Gender = employee.Gender,
                DepartmentId = employee.DepartmentId
            };

            this.data.Employees.Add(employeeData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All(string searchTerm, string department)
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
                    e.Gender.ToLower().Contains(searchTerm.ToLower()));
            }

            var employees = employeesQuery
                .Select(e => new EmployeeListingViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    EmailAddress = e.EmailAddress,
                    PhoneNumber = e.PhoneNumber,
                    Nationality = e.Nationality,
                    DateOfBirth = e.DateOfBirth.ToShortDateString(),
                    Gender = e.Gender,
                    Department = e.Department.Name
                })
                .ToList();

            var departments = this
                .GetDepartments()
                .Select(d => d.Name)
                .OrderBy(d => d)
                .ToList();

            return View(new AllEmployeesQueryModel
            {
                Departments = departments,
                Employees = employees,
                SearchTerm = searchTerm
            });
        }

        private IEnumerable<EmployeeDepartmentViewModel> GetDepartments()
            => this.data
                .Departments
                .Select(d => new EmployeeDepartmentViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                })
                .ToList();
    }
}
