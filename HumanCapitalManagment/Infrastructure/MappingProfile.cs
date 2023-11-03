namespace HumanCapitalManagment.Infrastructure
{
    using AutoMapper;
    using HumanCapitalManagment.Data.Models;
    using HumanCapitalManagment.Models.Employees;
    using HumanCapitalManagment.Services.Employees.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            this.CreateMap<EmployeeDetailsServiceModel, EmployeeFormModel>();
            this.CreateMap<Employee, EmployeeDetailsServiceModel>()
                .ForMember(e => e.UserId, cfg => cfg.MapFrom(e => e.HRSpecialist.UserId));
        }
    }
}
