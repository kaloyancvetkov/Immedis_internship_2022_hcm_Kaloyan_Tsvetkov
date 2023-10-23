namespace HumanCapitalManagment.Controllers
{
    using HumanCapitalManagment.Data;
    using HumanCapitalManagment.Data.Models;
    using HumanCapitalManagment.Infrastructure;
    using HumanCapitalManagment.Models.Employees;
    using HumanCapitalManagment.Services.Employees;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class EmployeesController : Controller
    {
        private readonly HCMDbContext data;
        private readonly IEmployeeService employees;

        public EmployeesController(HCMDbContext data, IEmployeeService employees)
        {
            this.data = data;
            this.employees = employees;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsHR())
            {
                return RedirectToAction(nameof(HRSpecialistsController.Become), "HRsController");
            }

            return View(new AddEmployeeFormModel
            {
                Departments = this.GetDepartments()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddEmployeeFormModel employee)
        {
            var hrId = this.data
                .HRSpecialists
                .Where(h => h.UserId == this.User.GetId())
                .Select(h => h.Id)
                .FirstOrDefault();

            if (hrId == 0)
            {
                return RedirectToAction(nameof(HRSpecialistsController.Become), "HRsController");
            }

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
                DepartmentId = employee.DepartmentId,
                HRSpecialistId = hrId
            };

            this.data.Employees.Add(employeeData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllEmployeesQueryModel query)
        {
            var queryResult = this.employees.All(
                query.Department,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllEmployeesQueryModel.EmployeesPerPage);

            var departments = this.employees.AllDepartments();

            query.Departments = departments;
            query.Employees = queryResult.Employees;
            query.TotalEmployees = queryResult.TotalEmployees;

            return View(query);
        }

        private bool UserIsHR()
            => this.data
                .HRSpecialists
                .Any(h => h.UserId == this.User.GetId());

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
