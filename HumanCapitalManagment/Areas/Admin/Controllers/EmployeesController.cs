namespace HumanCapitalManagment.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class EmployeesController : AdminController
    {
        public IActionResult Index() => View();
    }
}
