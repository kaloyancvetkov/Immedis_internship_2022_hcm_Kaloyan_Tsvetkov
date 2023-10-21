namespace HumanCapitalManagment.Controllers.Api
{
    using HumanCapitalManagment.Data;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/employees")]
    public class EmployeesApiController : ControllerBase
    {
        private readonly HCMDbContext data;

        public EmployeesApiController(HCMDbContext data) 
            => this.data = data;


    }
}
