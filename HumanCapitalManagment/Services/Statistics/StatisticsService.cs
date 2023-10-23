namespace HumanCapitalManagment.Services.Statistics
{
    using HumanCapitalManagment.Data;
    using System.Linq;

    public class StatisticsService : IStatisticsService
    {
        private readonly HCMDbContext data;

        public StatisticsService(HCMDbContext data)
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalEmployees = this.data.Employees.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalEmployees = totalEmployees,
                TotalUsers = totalUsers,
            };
        }
    }
}
