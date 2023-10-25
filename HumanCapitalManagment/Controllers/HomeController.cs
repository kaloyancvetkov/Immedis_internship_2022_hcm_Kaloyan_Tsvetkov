namespace HumanCapitalManagment.Controllers
{
    using HumanCapitalManagment.Data;
    using HumanCapitalManagment.Models;
    using HumanCapitalManagment.Models.Home;
    using HumanCapitalManagment.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly HCMDbContext data;
        private readonly IStatisticsService statistics;

        public HomeController(IStatisticsService statistics, HCMDbContext data) 
        {
            this.statistics = statistics;
            this.data = data;
        }

        public IActionResult Index()
        {
            var totalEmployees = this.data.Employees.Count();
            var totalUsers = this.data.Users.Count();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalEmployees = totalStatistics.TotalEmployees,
                TotalUsers = totalStatistics.TotalUsers,
                TotalCandidates = totalStatistics.TotalCandidates
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
