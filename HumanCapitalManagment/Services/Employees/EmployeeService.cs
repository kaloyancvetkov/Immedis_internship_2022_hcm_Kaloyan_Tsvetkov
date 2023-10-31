namespace HumanCapitalManagment.Services.Employees
{
    using HumanCapitalManagment.Data;
    using HumanCapitalManagment.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;

    public class EmployeeService : IEmployeeService
    {
        private readonly HCMDbContext data;

        public EmployeeService(HCMDbContext data)
            => this.data = data;

        public EmployeeQueryServiceModel All(
            string department,
            string searchTerm,
            EmployeesSorting sorting,
            int currentPage,
            int employeesPerPage)
        {
            var employeesQuery = this.data.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(department))
            {
                employeesQuery = employeesQuery.Where(e => e.Department.Name == department);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                employeesQuery = employeesQuery.Where(e =>
                    e.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    e.EmailAddress.ToLower().Contains(searchTerm.ToLower()) ||
                    e.PhoneNumber.Contains(searchTerm) ||
                    e.Nationality.ToLower().Contains(searchTerm.ToLower()) ||
                    e.DateOfBirth.ToString().Contains(searchTerm) ||
                    e.SalaryStatus.ToLower().Contains(searchTerm.ToLower()) ||
                    e.Gender.ToLower().Contains(searchTerm.ToLower()));
            }

            employeesQuery = sorting switch
            {
                EmployeesSorting.Name => employeesQuery.OrderBy(e => e.Name),
                EmployeesSorting.Department => employeesQuery.OrderBy(e => e.Department.Name),
                EmployeesSorting.Nationality => employeesQuery.OrderBy(e => e.Nationality),
                EmployeesSorting.DateOfBirth => employeesQuery.OrderBy(e => e.DateOfBirth),
                EmployeesSorting.SalaryAmount => employeesQuery.OrderByDescending(e => e.SalaryAmount),
                EmployeesSorting.DateAdded or _ => employeesQuery.OrderBy(e => e.Id)
            };

            var totalEmployees = employeesQuery.Count();

            var employees = GetEmployees(employeesQuery
                .Skip((currentPage - 1) * employeesPerPage)
                .Take(employeesPerPage));


            return new EmployeeQueryServiceModel
            {
                TotalEmployees = totalEmployees,
                CurrentPage = currentPage,
                EmployeesPerPage = employeesPerPage,
                Employees = employees
            };
        }

        public EmployeeDetailsServiceModel Details(int id)
            => this.data
                .Employees
                .Where(e => e.Id == id)
                .Select(e => new EmployeeDetailsServiceModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    EmailAddress = e.EmailAddress,
                    PhoneNumber = e.PhoneNumber,
                    Nationality = e.Nationality,
                    DateOfBirth = e.DateOfBirth,
                    Gender = e.Gender,
                    DepartmentName = e.Department.Name,
                    SalaryAmount = e.SalaryAmount,
                    SalaryStatus = e.SalaryStatus,
                    HRSpecialistId = e.HRSpecialistId,
                    HRSpecialistName = e.HRSpecialist.Name,
                    UserId = e.HRSpecialist.UserId
                })
                .FirstOrDefault();

        public int Create(
                string name,
                string emailAddress,
                string phoneNumber,
                string nationality,
                DateTime dateOfBirth,
                string gender,
                int departmentId,
                int hrId,
                decimal salaryAmount,
                string salaryStatus)
        {
            var employeeData = new Employee
            {
                Name = name,
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber,
                Nationality = nationality,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                DepartmentId = departmentId,
                HRSpecialistId = hrId,
                SalaryAmount = salaryAmount,
                SalaryStatus = salaryStatus
            };

            this.data.Employees.Add(employeeData);
            this.data.SaveChanges();

            return employeeData.Id;
        }

        public bool Edit(
                int id,
                string name,
                string emailAddress,
                string phoneNumber,
                string nationality,
                DateTime dateOfBirth,
                string gender,
                int departmentId,
                decimal salaryAmount,
                string salaryStatus)
        {
            var employeeData = this.data.Employees.Find(id);

            if (employeeData == null)
            {
                return false;
            }

            employeeData.Name = name;
            employeeData.EmailAddress = emailAddress;
            employeeData.PhoneNumber = phoneNumber;
            employeeData.Nationality = nationality;
            employeeData.DateOfBirth = dateOfBirth;
            employeeData.Gender = gender;
            employeeData.DepartmentId = departmentId;
            employeeData.SalaryAmount = salaryAmount;
            employeeData.SalaryStatus = salaryStatus;

            this.data.SaveChanges();

            return true;
        }
        public bool IsByHR(int employeeId, int hrId)
            => this.data
                .Employees
                .Any(e => e.Id == employeeId && e.HRSpecialistId == hrId);

        public IEnumerable<EmployeeServiceModel> ByUser(string userId)
            => this.GetEmployees(this.data
                .Employees
                .Where(e => e.HRSpecialist.UserId == userId));



        public IEnumerable<string> AllDepartmentNames()
            => this.data
                .Departments
                .Select(d => d.Name)
                .OrderBy(d => d)
                .ToList();

        private IEnumerable<EmployeeServiceModel> GetEmployees(IQueryable<Employee> employeesQuery)
            => employeesQuery
                .Select(e => new EmployeeServiceModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    EmailAddress = e.EmailAddress,
                    PhoneNumber = e.PhoneNumber,
                    Nationality = e.Nationality,
                    DateOfBirth = e.DateOfBirth,
                    Gender = e.Gender,
                    DepartmentName = e.Department.Name,
                    SalaryAmount = e.SalaryAmount,
                    SalaryStatus = e.SalaryStatus,
                })
            .ToList();

        public IEnumerable<EmployeeDepartmentServiceModel> AllDepartments()
            => this.data
                .Departments
                .Select(d => new EmployeeDepartmentServiceModel
                {
                    Id = d.Id,
                    Name = d.Name,
                })
                .ToList();

        public bool DepartmentExists(int departmentId)
            => this.data
                .Departments
                .Any(c => c.Id == departmentId);


    }
}
