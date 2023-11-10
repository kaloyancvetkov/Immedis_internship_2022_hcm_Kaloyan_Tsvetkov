namespace HumanCapitalManagment.Controllers
{
    using HumanCapitalManagment.Data;
    using HumanCapitalManagment.Data.Models;
    using HumanCapitalManagment.Infrastructure.Extensions;
    using HumanCapitalManagment.Models.HRSpecialists;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class HRSpecialistsController : Controller
    {
        private readonly HCMDbContext data;

        public HRSpecialistsController(HCMDbContext data) 
            => this.data = data;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeHRSpecialistFormModel hrSpecialist)
        {
            var userId = this.User.Id();

            var userIsAlreadyHR = this.data
                .HRSpecialists
                .Any(h => h.UserId == userId);

            if (userIsAlreadyHR)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(hrSpecialist);
            }

            var hrSpecialistData = new HRSpecialist
            {
                Name = hrSpecialist.Name,
                PhoneNumber = hrSpecialist.PhoneNumber,
                UserId = userId,
            };

            this.data.HRSpecialists.Add(hrSpecialistData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Employees");
        }
    }
}
