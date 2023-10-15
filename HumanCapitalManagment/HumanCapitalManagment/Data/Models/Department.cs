namespace HumanCapitalManagment.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Department
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<Employee> Employees { get; init; } = new List<Employee>();
    }
}
