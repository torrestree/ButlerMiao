using Microsoft.EntityFrameworkCore;

namespace Core.Misc.EF
{
    public abstract class ContextConfigBase
    {
        public string Server { get; set; } = GlobalVariables.DesignServer;
        public string User { get; set; } = GlobalVariables.DesignUser;
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
