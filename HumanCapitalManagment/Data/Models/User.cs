using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static HumanCapitalManagment.Data.DataConstants.User;

namespace HumanCapitalManagment.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength)]
        public string FullName { get; set; }
    }
}
