namespace HumanCapitalManagment.Services.Employees
{
    using HumanCapitalManagment.Models;
    using HumanCapitalManagment.Services.Employees.Models;
    using System;
    using System.Collections.Generic;

    public interface IEmployeeService
    {
        EmployeeQueryServiceModel All(
            string department = null,
            string searchTerm = null,
            EmployeesSorting sorting = EmployeesSorting.DateAdded,
            int currentPage = 1,
            int employeesPerPage = int.MaxValue,
            bool publicOnly = true);

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
                string SalaryStatus,
                bool isPublic);

        bool Delete(int employeeId);

        bool IsByHR(int employeeId, int hrId);

        void ChangeVisibility(int employeeId);

        IEnumerable<EmployeeServiceModel> ByUser(string userId);

        IEnumerable<string> AllDepartmentNames();

        IEnumerable<EmployeeDepartmentServiceModel> AllDepartments();

        bool DepartmentExists(int departmentId);
    }
}
