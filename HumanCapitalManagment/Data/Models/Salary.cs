using System;

namespace HumanCapitalManagment.Data.Models
{
    public class Salary
    {
        public int Id { get; set; }

        public string SalaryNote { get; set; }

        public DateTime? DateAdded { get; set; }

        public decimal Amount { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; init; }
    }
}
