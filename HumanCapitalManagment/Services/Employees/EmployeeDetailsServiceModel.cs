namespace HumanCapitalManagment.Services.Employees
{
    public class EmployeeDetailsServiceModel : EmployeeServiceModel
    {
        public int HRSpecialistId { get; init; }

        public string HRSpecialistName { get; init; }

        public int DepartmentId { get; set; }

        public string UserId { get; init; }
    }
}
