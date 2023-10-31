namespace HumanCapitalManagment.Services.HRs
{
    using HumanCapitalManagment.Data;
    using System.Linq;

    public class HRService : IHRService
    {
        private readonly HCMDbContext data;

        public HRService(HCMDbContext data) 
            => this.data = data;

        public int IdByUser(string userId)
           => this.data
                .HRSpecialists
                .Where(h => h.UserId == userId)
                .Select(h => h.Id)
                .FirstOrDefault();

        public bool IsHRSpecialist(string userId)
            => this.data
            .HRSpecialists
            .Any(h => h.UserId == userId);
    }
}
