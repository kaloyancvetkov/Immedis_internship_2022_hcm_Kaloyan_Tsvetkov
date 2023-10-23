namespace HumanCapitalManagment.Services.Candidates
{
    using HumanCapitalManagment.Models.Candidates;
    using System.Collections.Generic;


    public interface ICandidateService
    {
        CandidateQueryServiceModel All(
            string department,
            string searchTerm,
            CandidatesSorting sorting,
            int currentPage,
            int candidatesPerPage);

        IEnumerable<string> AllDepartments();
    }
}
