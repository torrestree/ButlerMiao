using Core.Misc.EA;

namespace Core.Misc.EA.Processor
{
    public class EFExceptionProcessor : IExceptionProcessor
    {
        public const string Source = "ef";

        public string Process(Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}
