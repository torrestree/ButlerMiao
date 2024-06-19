using Core.Misc.EA.Processor;

namespace Core.Misc.EA
{
    public abstract class ExceptionAnalyzerCore : IExceptionAnalyzer
    {
        protected Dictionary<string, IExceptionProcessor> Processors { get; set; } = [];

        protected ExceptionAnalyzerCore()
        {
            Processors.Add(EFExceptionProcessor.Source, new EFExceptionProcessor());
        }
        public string Analyze(Exception ex)
        {
            if (Processors.SingleOrDefault(t => t.Key == ex.Source) is KeyValuePair<string, IExceptionProcessor> pair)
            {
                return pair.Value.Process(ex);
            }
            else
            {
                return $"未知错误\n{ex.ToString}";
            }
        }
    }
}
