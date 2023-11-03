namespace HumanCapitalManagment.Services.Employees.Models
{
    using System.Collections.Generic;

    public class EmployeeQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int EmployeesPerPage { get; init; }

        public int TotalEmployees { get; init; }

        public IEnumerable<EmployeeServiceModel> Employees { get; init; }
    }
}
