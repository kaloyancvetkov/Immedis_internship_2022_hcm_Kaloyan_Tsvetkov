namespace HumanCapitalManagment.Services.Employees
{
    using HumanCapitalManagment.Models;
    using HumanCapitalManagment.Services.Employees.Models;
    using System;
    using System.Collections.Generic;

    public interface IEmployeeService
    {
        EmployeeQueryServiceModel All(
            string department,
            string searchTerm,
            EmployeesSorting sorting,
            int currentPage,
        int employeesPerPage);

        EmployeeDetailsServiceModel Details(int employeeId);

        int Create(string Name,
                string EmailAddress,
                string PhoneNumber,
                string Nationality,
                DateTime DateOfBirth,
                string Gender,
                int DepartmentId,
                int hrId,
                decimal SalaryAmount,
                string SalaryStatus);

        bool Edit(int employeeId,
                string Name,
                string EmailAddress,
                string PhoneNumber,
                string Nationality,
                DateTime DateOfBirth,
                string Gender,
                int DepartmentId,
                decimal SalaryAmount,
                string SalaryStatus);

        bool IsByHR(int employeeId, int hrId);

        IEnumerable<EmployeeServiceModel> ByUser(string userId);

        IEnumerable<string> AllDepartmentNames();

        IEnumerable<EmployeeDepartmentServiceModel> AllDepartments();

        bool DepartmentExists(int departmentId);
    }
}
