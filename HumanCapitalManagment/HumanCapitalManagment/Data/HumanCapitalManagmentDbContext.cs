namespace HumanCapitalManagment.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class HumanCapitalManagmentDbContext : IdentityDbContext
    {
        public HumanCapitalManagmentDbContext(DbContextOptions<HumanCapitalManagmentDbContext> options)
            : base(options)
        {
        }
    }
}
