namespace HumanCapitalManagment.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Employee;

    public class Employee
    {
        public int Id { get; init; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(NationalityMaxLength, MinimumLength = NationalityMinLength)]
        public string Nationality { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; init; }

    }
}
