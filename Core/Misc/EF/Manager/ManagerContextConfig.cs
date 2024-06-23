namespace Core.Misc.EF.Manager
{
    public class ManagerContextConfig : ContextConfig
    {
        public void Set(string server, string user, string password)
        {
            Server = server;
            User = user;
            Password = password;
            Database = ConstTableCore.ManagerDatabaseFormat;
        }
    }
}
