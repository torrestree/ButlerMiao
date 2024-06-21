using Core.Misc.EA;
using CoreServer.Misc.DI;
using CoreServer.ViewModel.Common;
using Microsoft.Extensions.DependencyInjection;
using ServerWpf.Misc.EA;
using ServerWpf.View.Common;

namespace ServerWpf.Misc.DI
{
    public class DIRegister : DIRegisterCoreServer
    {
        protected override void RegisterMisc(IServiceCollection services)
        {
            base.RegisterMisc(services);
            services
                .AddSingleton<MainWindow>()
                .AddSingleton<IExceptionAnalyzer, ExceptionAnalyzer>()
                ;
        }
        protected override void RegisterViewModel(IServiceCollection services)
        {
            base.RegisterViewModel(services);
            services
                .AddSingleton<VmSetInfosEditor>()
                ;
        }
        protected override void RegisterView(IServiceCollection services)
        {
            base.RegisterView(services);
            services
                .AddTransient<UcSignIn>()
                .AddTransient<UcMain>()
                ;
        }
    }
}
