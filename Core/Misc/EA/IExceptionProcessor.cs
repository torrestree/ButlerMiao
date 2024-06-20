namespace Core.Misc.EA
{
    public interface IExceptionProcessor
    {
        bool Process(Exception ex, out string message);
    }
}
