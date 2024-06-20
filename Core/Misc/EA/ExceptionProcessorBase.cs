namespace Core.Misc.EA
{
    public abstract class ExceptionProcessorBase : IExceptionProcessor
    {
        public bool Process(Exception ex, out string message)
        {
            message = Process(ex.HResult);
            return message != string.Empty;
        }
        protected abstract string Process(int index);
    }
}
