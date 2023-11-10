namespace HumanCapitalManagment.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.HRSpecialist;
    public class HRSpecialist
    {
        public int Id { get; init; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string UserId { get; init; }

        public IEnumerable<Employee> Employees { get; init; } = new List<Employee>();
    }
}
