using Microsoft.EntityFrameworkCore;

namespace Core.Misc.EF.Set
{
    public class SetContext(SetContextConfig setContextConfig) : DbContext
    {
        private SetContextConfig SetContextConfig { get; set; } = setContextConfig;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SetContextConfig.Config(optionsBuilder);
        }
    }
}
