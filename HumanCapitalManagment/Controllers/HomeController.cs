namespace HumanCapitalManagment.Controllers
{
    using HumanCapitalManagment.Data;
    using HumanCapitalManagment.Models;
    using HumanCapitalManagment.Models.Home;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly HCMDbContext data;

        public HomeController(HCMDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index()
        {
            var totalEmployees = this.data.Employees.Count();

            return View(new IndexViewModel
            {
                TotalEmployees = totalEmployees,
                TotalUsers = 0,
                TotalCandidates = 0
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
