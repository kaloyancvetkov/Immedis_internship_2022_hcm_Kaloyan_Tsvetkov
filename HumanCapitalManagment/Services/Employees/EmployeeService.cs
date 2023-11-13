namespace HumanCapitalManagment.Services.Employees
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data.Models;
    using HumanCapitalManagment.Data;
    using HumanCapitalManagment.Models;
    using HumanCapitalManagment.Services.Employees.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EmployeeService : IEmployeeService
    {
        private readonly HCMDbContext data;
        private readonly IConfigurationProvider mapper;

        public EmployeeService(HCMDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public EmployeeQueryServiceModel All(
            string department = null,
            string searchTerm = null,
            EmployeesSorting sorting = EmployeesSorting.DateAdded,
            int currentPage = 1,
            int employeesPerPage = int.MaxValue,
            bool publicOnly = true)
        {
            var employeesQuery = this.data.Employees.Where(e => !publicOnly || e.IsPublic);

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
                .ProjectTo<EmployeeDetailsServiceModel>(this.mapper)
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
                SalaryStatus = salaryStatus,
                IsPublic = false
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
                string salaryStatus,
                bool isPublic)
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
            employeeData.IsPublic = isPublic;

            this.data.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var employee = this.data
                .Employees
                .FirstOrDefault(s => s.Id == id);

            if (employee != null)
            {
                this.data.Remove(employee);
                this.data.SaveChanges();
                return true;
            }

            return false;
        }


        public bool IsByHR(int employeeId, int hrId)
            => this.data
                .Employees
                .Any(e => e.Id == employeeId && e.HRSpecialistId == hrId);

        public void ChangeVisibility(int employeeId)
        {
            var employee = this.data.Employees.Find(employeeId);

            employee.IsPublic = !employee.IsPublic;

            this.data.SaveChanges();
        }

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
                .ProjectTo<EmployeeServiceModel>(this.mapper)
                .ToList();

        public IEnumerable<EmployeeDepartmentServiceModel> AllDepartments()
            => this.data
                .Departments
                .ProjectTo<EmployeeDepartmentServiceModel>(this.mapper)
                .ToList();

        public bool DepartmentExists(int departmentId)
            => this.data
                .Departments
                .Any(c => c.Id == departmentId);


    }
}
