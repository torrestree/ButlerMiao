using Core.Misc.EA;
using Core.Misc.EF.Manager;
using Core.Misc.EF.Set;
using Core.ViewModel.Common;
using CoreServer.Misc.DI;
using CoreServer.ViewModel.Common;
using HelperWpf.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using ServerWpf.Misc.EA;
using ServerWpf.View.Common.SignIn;
using ServerWpf.View.Common.Workspace;
using ServerWpf.ViewModel;

namespace ServerWpf.Misc.DI
{
    public class DIRegister : DIRegisterCoreServer
    {
        protected override void RegisterMisc(IServiceCollection services)
        {
            base.RegisterMisc(services);
            services
                .AddSingleton<IExceptionAnalyzer, ExceptionAnalyzer>()
                .AddSingleton<ManagerContextConfig>()
                .AddDbContext<ManagerContext>(ServiceLifetime.Transient, ServiceLifetime.Singleton)
                .AddScoped<SetContextConfig>()
                .AddDbContext<SetContext>(ServiceLifetime.Transient, ServiceLifetime.Scoped)
                ;
        }
        protected override void RegisterViewModel(IServiceCollection services)
        {
            base.RegisterViewModel(services);
            services
                .AddSingleton<IVmMain, VmMain>()
                .AddTransient<IVmMessage, VmMessage>()
                ;
        }
        protected override void RegisterView(IServiceCollection services)
        {
            base.RegisterView(services);
            services
                .AddTransient<ViewSignIn>()
                .AddTransient<ViewWorkspace>()
                ;
        }
    }
}
