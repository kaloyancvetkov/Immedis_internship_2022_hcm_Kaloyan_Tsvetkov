namespace HumanCapitalManagment.Models.HRSpecialists
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.HRSpecialist;

    public class BecomeHRSpecialistFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
    }
}
