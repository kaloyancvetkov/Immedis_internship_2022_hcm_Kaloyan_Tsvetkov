namespace HumanCapitalManagment.Controllers
{
    using HumanCapitalManagment.Data;
    using HumanCapitalManagment.Data.Models;
    using HumanCapitalManagment.Models.Candidates;
    using HumanCapitalManagment.Models.Employees;
    using HumanCapitalManagment.Services.Candidates;
    using HumanCapitalManagment.Services.Employees;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class CandidatesController : Controller
    {
        private readonly HCMDbContext data;
        private readonly ICandidateService candidates;

        public CandidatesController(HCMDbContext data, ICandidateService candidates)
        {
            this.data = data;
            this.candidates = candidates;
        }

        [Authorize]
        public IActionResult Add()
        {
            return View(new AddCandidateFormModel
            {
                Departments = this.GetDepartments()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddCandidateFormModel candidate)
        {
            if (!this.data.Departments.Any(d => d.Id == candidate.DepartmentId))
            {
                this.ModelState.AddModelError(nameof(candidate.DepartmentId), "Department doesn't exist.");
            }

            if (!ModelState.IsValid)
            {
                candidate.Departments = this.GetDepartments();

                return View(candidate);
            }

            var candidateData = new Candidate
            {
                Name = candidate.Name,
                EmailAddress = candidate.EmailAddress,
                PhoneNumber = candidate.PhoneNumber,
                Nationality = candidate.Nationality,
                DateOfBirth = candidate.DateOfBirth,
                Gender = candidate.Gender,
                DepartmentId = candidate.DepartmentId,
            };

            this.data.Candidates.Add(candidateData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllCandidatesQueryModel query)
        {
            var queryResult = this.candidates.All(
                query.Department,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllCandidatesQueryModel.CandidatesPerPage);

            var departments = this.candidates.AllDepartments();

            query.Departments = departments;
            query.Candidates= queryResult.Candidates;
            query.TotalCandidates= queryResult.TotalCandidates;

            return View(query);
        }

        private IEnumerable<CandidateDepartmentViewModel> GetDepartments()
            => this.data
                .Departments
                .Select(d => new CandidateDepartmentViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                })
                .ToList();
    }
}
