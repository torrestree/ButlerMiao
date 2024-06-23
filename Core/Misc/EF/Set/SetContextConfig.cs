namespace Core.Misc.EF.Set
{
    public class SetContextConfig : ContextConfig
    {
        public void Set(string server, string user, string password, int index)
        {
            Server = server;
            User = user;
            Password = password;
            Database = string.Format(ConstTableCore.SetDatabaseFormat, index);
        }
    }
}
