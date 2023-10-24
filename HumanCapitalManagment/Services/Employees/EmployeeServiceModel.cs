namespace HumanCapitalManagment.Services.Employees
{
    public class EmployeeServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Nationality { get; set; }

        public string DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string Department { get; set; }

        public decimal SalaryAmount { get; set; }

        public string SalaryStatus { get; set; }
    }
}
