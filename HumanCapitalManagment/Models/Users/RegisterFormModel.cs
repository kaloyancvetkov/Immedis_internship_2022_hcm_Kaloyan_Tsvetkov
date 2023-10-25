namespace HumanCapitalManagment.Models.Users
{
    using System.ComponentModel.DataAnnotations;
    using static HumanCapitalManagment.Data.DataConstants.User;

    public class RegisterFormModel
    {
        [Required]
        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }

        [Required]
        [Compare("Password", ErrorMessage = "The confirm password and the password must be the same.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; init; }

    }
}
