using Core.Misc.EA;
using CoreServer.Misc.DI;
using Microsoft.Extensions.DependencyInjection;
using ServerWpf.Misc.EA;

namespace ServerWpf.Misc.DI
{
    public class DIRegister : DIRegisterCoreServer
    {
        protected override void RegisterMisc(IServiceCollection services)
        {
            base.RegisterMisc(services);
            services
                .AddSingleton<IExceptionAnalyzer, ExceptionAnalyzer>()
                ;
        }
    }
}
