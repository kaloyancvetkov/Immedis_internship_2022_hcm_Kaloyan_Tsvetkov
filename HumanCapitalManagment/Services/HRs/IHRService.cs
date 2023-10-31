namespace HumanCapitalManagment.Services.HRs
{
    public interface IHRService
    {
        public bool IsHRSpecialist(string userId);

        public int IdByUser(string userId);
    }
}
