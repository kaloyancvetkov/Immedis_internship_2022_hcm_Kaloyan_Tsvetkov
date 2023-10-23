namespace HumanCapitalManagment.Models.Candidates
{
    using HumanCapitalManagment.Services.Candidates;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllCandidatesQueryModel
    {
        public const int CandidatesPerPage = 10;

        public string Department { get; init; }

        public IEnumerable<string> Departments { get; set; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public CandidatesSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalCandidates { get; set; }

        public IEnumerable<CandidateServiceModel> Candidates { get; set; }
    }
}
