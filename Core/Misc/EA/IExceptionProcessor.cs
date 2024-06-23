namespace Core.Misc.EA
{
    public interface IExceptionProcessor
    {
        bool Process(Exception ex, out string message);
    }

    public abstract class ExceptionProcessor : IExceptionProcessor
    {
        public bool Process(Exception ex, out string message)
        {
            message = Process(ex.HResult);
            return message != string.Empty;
        }
        protected abstract string Process(int errorCode);
    }
}
