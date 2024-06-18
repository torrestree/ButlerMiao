using Core.Misc;
using Microsoft.EntityFrameworkCore;

namespace Core.EF
{
    public abstract class ContextConfigBase
    {
        public string Server { get; set; } = GlobalVariablesCore.EF.DesignServer;
        public string User { get; set; } = GlobalVariablesCore.EF.DesignUser;
        public string Password { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;

        public void Config(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = $"server={Server};user={User};password={Password};database={Database}";
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseMySql(connection, ServerVersion.AutoDetect(connection));
        }
    }
}
