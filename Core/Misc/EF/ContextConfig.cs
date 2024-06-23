using Microsoft.EntityFrameworkCore;

namespace Core.Misc.EF
{
    public abstract class ContextConfig
    {
        public string Server { get; set; } = ConstTableCore.DesignServer;
        public string User { get; set; } = ConstTableCore.DesignUser;
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
