namespace Core.Misc.EF.Manager
{
    public class ManagerContextConfig : ContextConfigBase
    {
        public void Set(string server, string user, string password)
        {
            Server = server;
            User = user;
            Password = password;
            Database = GlobalVariables.ManagerDatabaseFormat;
        }
    }
}
