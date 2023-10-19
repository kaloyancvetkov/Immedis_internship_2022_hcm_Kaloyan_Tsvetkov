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

        public IActionResult All([FromQuery]AllEmployeesQueryModel query)
        {
            var employeesQuery = this.data.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Department))
            {
                employeesQuery = employeesQuery.Where(e => e.Department.Name == query.Department);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                employeesQuery = employeesQuery.Where(e => 
                    e.Name.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    e.EmailAddress.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    e.PhoneNumber.Contains(query.SearchTerm) ||
                    e.Nationality.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    e.DateOfBirth.ToString().Contains(query.SearchTerm) ||
                    e.Gender.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            employeesQuery = query.Sorting switch
            {
                EmployeesSorting.Name => employeesQuery.OrderBy(e => e.Name),
                EmployeesSorting.Department => employeesQuery.OrderBy(e => e.Department.Name),
                EmployeesSorting.Nationality => employeesQuery.OrderBy(e => e.Nationality),
                EmployeesSorting.DateOfBirth => employeesQuery.OrderBy(e => e.DateOfBirth),
                EmployeesSorting.DateAdded or _ => employeesQuery.OrderBy(e => e.Id)
            };

            var employees = employeesQuery
                .Skip((query.CurrentPage - 1) * AllEmployeesQueryModel.EmployeesPerPage)
                .Take(AllEmployeesQueryModel.EmployeesPerPage)
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

            var totalEmployees = employeesQuery.Count();

            var departments = this
                .GetDepartments()
                .Select(d => d.Name)
                .OrderBy(d => d)
                .ToList();

            query.Departments = departments;
            query.Employees = employees;
            query.TotalEmployees = totalEmployees;

            return View(query);
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
