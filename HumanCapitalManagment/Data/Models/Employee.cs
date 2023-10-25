namespace HumanCapitalManagment.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Employee;

    public class Employee
    {
        public int Id { get; init; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "The name must be between {2} and {1} characters")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(NationalityMaxLength, MinimumLength = NationalityMinLength, ErrorMessage = "The nationality must be between {2} and {1} characters")]
        public string Nationality { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Invalid date")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "You must check one of the two options")]
        public string Gender { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; init; }

        public int HRSpecialistId { get; init; }
        public HRSpecialist HRSpecialist { get; init; }

        [Required]
        public decimal SalaryAmount { get; set; }

        [MaxLength(SalaryStatusMaxLength)]
        public string SalaryStatus { get; set; }
    }
}
