namespace HumanCapitalManagment.Services.Employees
{
    using HumanCapitalManagment.Models;
    using System.Collections.Generic;

    public interface IEmployeeService
    {
        EmployeeQueryServiceModel All(
            string department,
            string searchTerm,
            EmployeesSorting sorting,
            int currentPage,
            int employeesPerPage);

        IEnumerable<string> AllDepartments();
    }
}
