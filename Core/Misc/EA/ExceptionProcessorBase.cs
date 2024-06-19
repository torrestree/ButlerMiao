namespace Core.Misc.EA
{
    public abstract class ExceptionProcessorBase : IExceptionProcessor
    {
        public abstract string Process(Exception ex);
    }
}
