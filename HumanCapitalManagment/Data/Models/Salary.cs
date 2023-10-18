namespace HumanCapitalManagment.Data.Models
{
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static HumanCapitalManagment.Data.DataConstants.Salary;

    public class Salary
    {
        public int Id { get; set; }

        [MaxLength(NoteMaxLength)]
        public string SalaryNote { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateAdded { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; init; }
    }
}
