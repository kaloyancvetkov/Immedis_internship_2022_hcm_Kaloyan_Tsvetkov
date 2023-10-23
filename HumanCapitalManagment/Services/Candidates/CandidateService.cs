namespace HumanCapitalManagment.Services.Candidates
{
    using HumanCapitalManagment.Data;
    using HumanCapitalManagment.Models.Candidates;
    using System.Collections.Generic;
    using System.Linq;

    public class CandidateService : ICandidateService
    {
        private readonly HCMDbContext data;

        public CandidateService(HCMDbContext data)
            => this.data = data;

        public CandidateQueryServiceModel All(
            string department,
            string searchTerm,
            CandidatesSorting sorting,
            int currentPage,
            int candidatesPerPage)
        {
            var candidatesQuery = this.data.Candidates.AsQueryable();

            if (!string.IsNullOrWhiteSpace(department))
            {
                candidatesQuery = candidatesQuery.Where(c => c.Department.Name == department);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                candidatesQuery = candidatesQuery.Where(e =>
                    e.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    e.EmailAddress.ToLower().Contains(searchTerm.ToLower()) ||
                    e.PhoneNumber.Contains(searchTerm) ||
                    e.Nationality.ToLower().Contains(searchTerm.ToLower()) ||
                    e.DateOfBirth.ToString().Contains(searchTerm) ||
                    e.Gender.ToLower().Contains(searchTerm.ToLower()));
            }

            candidatesQuery = sorting switch
            {
                CandidatesSorting.Name => candidatesQuery.OrderBy(e => e.Name),
                CandidatesSorting.Department => candidatesQuery.OrderBy(e => e.Department.Name),
                CandidatesSorting.Nationality => candidatesQuery.OrderBy(e => e.Nationality),
                CandidatesSorting.DateOfBirth => candidatesQuery.OrderBy(e => e.DateOfBirth),
                CandidatesSorting.DateAdded or _ => candidatesQuery.OrderBy(e => e.Id)
            };

            var totalCandidates = candidatesQuery.Count();

            var candidates = candidatesQuery
                .Skip((currentPage - 1) * candidatesPerPage)
                .Take(candidatesPerPage)
                .Select(e => new CandidateServiceModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    EmailAddress = e.EmailAddress,
                    PhoneNumber = e.PhoneNumber,
                    Nationality = e.Nationality,
                    DateOfBirth = e.DateOfBirth.ToShortDateString(),
                    Gender = e.Gender,
                    Department = e.Department.Name
                })
                .ToList();

            return new CandidateQueryServiceModel
            {
                TotalCandidates = totalCandidates,
                CurrentPage = currentPage,
                CandidatesPerPage = candidatesPerPage,
                Candidates = candidates
            };
        }

        public IEnumerable<string> AllDepartments()
            => this.data
                .Departments
                .Select(d => d.Name)
                .OrderBy(d => d)
                .ToList();
    }
}