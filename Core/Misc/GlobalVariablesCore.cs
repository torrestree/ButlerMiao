namespace Core.Misc
{
    public class GlobalVariablesCore
    {
        public static EFVariables EF { get; set; }
    }
    public readonly struct EFVariables
    {
        public readonly string DesignServer = "localhost";
        public readonly string DesignUser = "designer";
        public readonly string ManagerDatabaseFormat = "ButlerMiaoManager";
        public readonly string SetDatabaseFormat = "ButlerMiaoSet_{0}";

        public EFVariables() { }
    }
}
