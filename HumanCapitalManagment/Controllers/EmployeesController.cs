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

            return RedirectToAction("Index", "Home");
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
