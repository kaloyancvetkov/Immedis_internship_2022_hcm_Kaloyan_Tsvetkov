using HumanCapitalManagment.Data.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace HumanCapitalManagment.Models.Employees
{
    public class AddEmployeeFormModel
    {
        public string Name { get; init; }

        public string Nationality { get; init; }

        [Display(Name = "Date of birth")]
        public DateTime? DateOfBirth { get; init; }

        public string Gender { get; init; }

        public int DepartmentId { get; init; }
    }
}
