namespace HumanCapitalManagment.Controllers
{
    using HumanCapitalManagment.Services.Employees;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DepartmentsController : Controller
    {
        private readonly IEmployeeService employees;

        public DepartmentsController(IEmployeeService employees)
        {
            this.employees = employees;
        }

        [Authorize]
        public IActionResult All()
        {
            var departments = this.employees
                .AllDepartments();

            return View(departments);
        }
    }
}
