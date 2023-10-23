namespace HumanCapitalManagment.Services.Candidates
{
    using System.Collections.Generic;

    public class CandidateQueryServiceModel
    {
            public int CurrentPage { get; init; }

            public int CandidatesPerPage { get; init; }

            public int TotalCandidates { get; init; }

            public IEnumerable<CandidateServiceModel> Candidates { get; init; }
    }
}
