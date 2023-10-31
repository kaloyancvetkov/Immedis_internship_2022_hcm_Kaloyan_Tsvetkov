namespace HumanCapitalManagment.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Department;

    public class Department
    {
        public int Id { get; init; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        public IEnumerable<Employee> Employees { get; init; } = new List<Employee>();
    }
}
