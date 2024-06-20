using Core.Misc.EA.Processor;

namespace Core.Misc.EA
{
    public abstract class ExceptionAnalyzerCore : IExceptionAnalyzer
    {
        protected Dictionary<string, IExceptionProcessor> Processors { get; set; } = [];

        protected ExceptionAnalyzerCore()
        {
            Processors.Add(MySqlConnectorProcessor.Source, new MySqlConnectorProcessor());
        }
        public string Analyze(Exception ex)
        {
            if (ex.Source == null)
            {
                return OutputUnknown(ex);
            }
            else
            {
                if (Processors.TryGetValue(ex.Source, out IExceptionProcessor? value))
                {
                    if (value.Process(ex, out string message))
                    {
                        return message;
                    }
                    else
                    {
                        return OutputUnknown(ex);
                    }
                }
                else
                {
                    return OutputUnknown(ex);
                }
            }

            static string OutputUnknown(Exception ex)
            {
                return $"未知错误\n错误源：{ex.Source}\n错误代号：{ex.HResult}\n错误内容：{ex.Message}";
            }
        }
    }
}
