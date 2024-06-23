namespace Core.Misc.EA.Processor
{
    public class MySqlConnectorProcessor : ExceptionProcessor
    {
        public const string Source = "MySqlConnector";

        protected override string Process(int errorCode)
        {
            return errorCode switch
            {
                -2147467259 => "无法连接数据库。\n请检查登录信息是否正确。",
                _ => string.Empty,
            };
        }
    }
}
