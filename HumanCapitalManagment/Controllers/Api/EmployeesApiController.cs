namespace HumanCapitalManagment.Controllers.Api
{
    using HumanCapitalManagment.Data;
    using HumanCapitalManagment.Models;
    using HumanCapitalManagment.Models.Api.Employees;
    using HumanCapitalManagment.Services.Employees;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    [ApiController]
    [Route("api/employees")]
    public class EmployeesApiController : ControllerBase
    {
        private readonly IEmployeeService employees;

        public EmployeesApiController(IEmployeeService employees) 
            => this.employees = employees;

        [HttpGet]
        public EmployeeQueryServiceModel All([FromQuery] AllEmployeesApiRequestModel query)
            => this.employees.All(
                query.Department,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.EmployeesPerPage);
    }
}
