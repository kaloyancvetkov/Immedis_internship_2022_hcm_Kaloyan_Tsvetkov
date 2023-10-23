using System.Collections.Generic;

namespace HumanCapitalManagment.Models.Api.Employees
{
    public class AllEmployeesApiRequestModel
    {
        public string Department { get; init; }

        public IEnumerable<string> Departments { get; set; }

        public string SearchTerm { get; init; }

        public EmployeesSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int EmployeesPerPage { get; init; } = 10;
    }
}
