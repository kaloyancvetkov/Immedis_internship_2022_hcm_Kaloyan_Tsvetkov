namespace HumanCapitalManagment.Models.Candidates
{
    using HumanCapitalManagment.Models.Employees;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static HumanCapitalManagment.Data.DataConstants.Candidate;

    public class AddCandidateFormModel
    {
        [Required(ErrorMessage = "The name is required")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "The name must be between {2} and {1} characters")]
        public string Name { get; init; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; init; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Phone Number")]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "The nationality is required")]
        [StringLength(NationalityMaxLength, MinimumLength = NationalityMinLength, ErrorMessage = "The nationality must be between {2} and {1} characters")]
        public string Nationality { get; init; }

        [Required(ErrorMessage = "You must provide a date of birth")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date")]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; init; }

        [Required(ErrorMessage = "You must check one of the two options")]
        public string Gender { get; init; }

        [Display(Name = "Department")]
        public int DepartmentId { get; init; }

        public IEnumerable<CandidateDepartmentViewModel> Departments { get; set; }
    }
}
