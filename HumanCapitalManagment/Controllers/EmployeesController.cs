namespace HumanCapitalManagment.Controllers
{
    using AutoMapper;
    using HumanCapitalManagment.Infrastructure.Extensions;
    using HumanCapitalManagment.Models.Employees;
    using HumanCapitalManagment.Services.Employees;
    using HumanCapitalManagment.Services.HRs;
    using Humanizer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static WebConstants;

    public class EmployeesController : Controller
    {
        private readonly IHRService hrSpecialists;
        private readonly IEmployeeService employees;
        private readonly IMapper mapper;

        public EmployeesController(IEmployeeService employees, IHRService hrSpecialists, IMapper mapper)
        {
            this.employees = employees;
            this.hrSpecialists = hrSpecialists;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.hrSpecialists.IsHRSpecialist(this.User.Id()))
            {
                return RedirectToAction(nameof(HRSpecialistsController.Become), "HRSpecialists");
            }

            return View(new EmployeeFormModel
            {
                Departments = this.employees.AllDepartments()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(EmployeeFormModel employee)
        {
            var hrId = this.hrSpecialists.IdByUser(this.User.Id());

            if (hrId == 0)
            {
                return RedirectToAction(nameof(HRSpecialistsController.Become), "HRSpecialists");
            }

            if (!this.employees.DepartmentExists(employee.DepartmentId))
            {
                this.ModelState.AddModelError(nameof(employee.DepartmentId), "Department doesn't exist.");
            }

            if (!ModelState.IsValid)
            {
                employee.Departments = this.employees.AllDepartments();

                return View(employee);
            }

            this.employees.Create(
                employee.Name,
                employee.EmailAddress,
                employee.PhoneNumber,
                employee.Nationality,
                employee.DateOfBirth,
                employee.Gender,
                employee.DepartmentId,
                hrId,
                employee.SalaryAmount,
                employee.SalaryStatus);

            TempData[GlobalMessageKey] = "Your employee record was added and is awaiting for approval!";

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.hrSpecialists.IsHRSpecialist(this.User.Id()) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(HRSpecialistsController.Become), "HRSpecialists");
            }

            var employee = this.employees.Details(id);

            if (employee.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var employeeForm = this.mapper.Map<EmployeeFormModel>(employee);

            employeeForm.Departments = this.employees.AllDepartments();

            return View(employeeForm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, EmployeeFormModel employee)
        {
            var hrId = this.hrSpecialists.IdByUser(this.User.Id());

            if (hrId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(HRSpecialistsController.Become), "HRSpecialists");
            }

            if (!this.employees.DepartmentExists(employee.DepartmentId))
            {
                this.ModelState.AddModelError(nameof(employee.DepartmentId), "Department doesn't exist.");
            }

            if (!ModelState.IsValid)
            {
                employee.Departments = this.employees.AllDepartments();

                return View(employee);
            }

            if (!this.employees.IsByHR(id, hrId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.employees.Edit(
                id,
                employee.Name,
                employee.EmailAddress,
                employee.PhoneNumber,
                employee.Nationality,
                employee.DateOfBirth,
                employee.Gender,
                employee.DepartmentId,
                employee.SalaryAmount,
                employee.SalaryStatus,
                this.User.IsAdmin());

            TempData[GlobalMessageKey] = $"Your employee record was edited{(this.User.IsAdmin() ? string.Empty : " and is awaiting for approval")}!";

            if (User.IsAdmin())
            {
                return RedirectToAction(nameof(All));
                
            }

            return RedirectToAction(nameof(Mine));
        }

        public IActionResult All([FromQuery] AllEmployeesQueryModel query)
        {
            var queryResult = this.employees.All(
                query.Department,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllEmployeesQueryModel.EmployeesPerPage);

            var departments = this.employees.AllDepartmentNames();

            query.Departments = departments;
            query.Employees = queryResult.Employees;
            query.TotalEmployees = queryResult.TotalEmployees;

            return View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myEmployees = this.employees.ByUser(this.User.Id());

            return View(myEmployees);
        }
    }
}
