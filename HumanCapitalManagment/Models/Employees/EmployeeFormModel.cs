namespace HumanCapitalManagment.Models.Employees
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Employee;
    using HumanCapitalManagment.Services.Employees;

    public class EmployeeFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "The name must be between {2} and {1} characters")]
        public string Name { get; init; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; init; }

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(NationalityMaxLength, MinimumLength = NationalityMinLength, ErrorMessage = "The nationality must be between {2} and {1} characters")]
        public string Nationality { get; init; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Invalid date")]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; init; }

        [Required(ErrorMessage = "You must check one of the two options")]
        public string Gender { get; init; }

        [Display(Name = "Department")]
        public int DepartmentId { get; init; }

        public IEnumerable<EmployeeDepartmentServiceModel> Departments { get; set; }

        [Required]
        [Display(Name = "Salary Amount")]
        public decimal SalaryAmount { get; set; }

        [Display(Name = "Salary Status")]
        [MaxLength(SalaryStatusMaxLength)]
        public string SalaryStatus { get; set; }
    }
}
