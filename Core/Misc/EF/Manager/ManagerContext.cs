using Microsoft.EntityFrameworkCore;

namespace Core.Misc.EF.Manager
{
    public class ManagerContext(ManagerContextConfig managerContextConfig) : DbContext
    {
        private ManagerContextConfig ManagerContextConfig { get; set; } = managerContextConfig;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ManagerContextConfig.Config(optionsBuilder);
        }
    }
}
