using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumanCapitalManagment.Models.Employees
{
    public class AllEmployeesQueryModel
    {
        public const int EmployeesPerPage = 2;

        public string Department { get; init; }

        public IEnumerable<string> Departments { get; set; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public EmployeesSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalEmployees { get; set; }

        public IEnumerable<EmployeeListingViewModel> Employees { get; set; }
    }
}
