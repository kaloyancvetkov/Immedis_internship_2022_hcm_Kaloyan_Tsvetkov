namespace HumanCapitalManagment.Services.Employees
{
    using System;

    public class EmployeeServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Nationality { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string DepartmentName { get; set; }

        public decimal SalaryAmount { get; set; }

        public string SalaryStatus { get; set; }
    }
}
