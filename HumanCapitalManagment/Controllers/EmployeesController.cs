namespace HumanCapitalManagment.Controllers
{
    using HumanCapitalManagment.Models.Employees;
    using Microsoft.AspNetCore.Mvc;

    public class EmployeesController : Controller
    {
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddEmployeeFormModel employee)
        {


            return View();
        }
    }
}
