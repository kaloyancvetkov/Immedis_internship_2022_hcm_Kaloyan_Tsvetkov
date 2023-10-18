using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumanCapitalManagment.Models.Employees
{
    public class AllEmployeesQueryModel
    {
        public string Department { get; init; }

        public IEnumerable<string> Departments { get; init; }

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public EmployeesSorting Sorting { get; init; }

        public IEnumerable<EmployeeListingViewModel> Employees { get; init; }
    }
}
