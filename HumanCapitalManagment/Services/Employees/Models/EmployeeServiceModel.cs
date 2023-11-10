namespace HumanCapitalManagment.Services.Employees.Models
{
    using System;

    public class EmployeeServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string EmailAddress { get; init; }

        public string PhoneNumber { get; init; }

        public string Nationality { get; init; }

        public DateTime DateOfBirth { get; init; }

        public string Gender { get; init; }

        public string DepartmentName { get; init; }

        public decimal SalaryAmount { get; init; }

        public string SalaryStatus { get; init; }

        public bool IsPublic { get; init; }
    }
}
