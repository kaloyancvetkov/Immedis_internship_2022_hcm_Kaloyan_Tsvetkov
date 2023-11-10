namespace HumanCapitalManagment.Areas.Admin.Controllers
{
    using HumanCapitalManagment.Services.Employees;
    using Microsoft.AspNetCore.Mvc;

    public class EmployeesController : AdminController
    {
        private readonly IEmployeeService employees;

        public EmployeesController(IEmployeeService employees) 
            => this.employees = employees;

        public IActionResult All()
        {
            var employees = this.employees
                .All(publicOnly: false)
                .Employees;

            return View(employees);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.employees.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
