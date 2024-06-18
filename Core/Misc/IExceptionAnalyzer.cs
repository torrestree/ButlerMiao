namespace Core.Misc
{
    public interface IExceptionAnalyzer
    {
        string Analyze(Exception ex);
    }

    public abstract class ExceptionAnalyzerCore : IExceptionAnalyzer
    {
        protected IDictionary<string, Func<Exception, string>> Analyzers { get; set; }

        public string Analyze(Exception ex)
        {
            var analyzer = Analyzers.SingleOrDefault(t => t.Key == ex.Source);
            
        }
    }
}
