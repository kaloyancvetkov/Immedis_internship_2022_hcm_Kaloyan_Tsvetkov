namespace HumanCapitalManagment.Models.Employees
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HumanCapitalManagment.Models;
    using HumanCapitalManagment.Services.Employees.Models;

    public class AllEmployeesQueryModel
    {
        public const int EmployeesPerPage = 10;

        public string Department { get; init; }

        public IEnumerable<string> Departments { get; set; }

        [Display(Name = "Search by key word")]
        public string SearchTerm { get; init; }

        public EmployeesSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalEmployees { get; set; }

        public IEnumerable<EmployeeServiceModel> Employees { get; set; }
    }
}
