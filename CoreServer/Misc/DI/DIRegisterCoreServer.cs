using Core.Misc.DI;
using Core.Misc.EF.Manager;
using Core.Misc.EF.Set;
using CoreServer.ViewModel.Common;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServer.Misc.DI
{
    public abstract class DIRegisterCoreServer : DIRegisterCore
    {
        protected override void RegisterMisc(IServiceCollection services)
        {
            base.RegisterMisc(services);
            services
                .AddSingleton<ManagerContextConfig>()
                .AddDbContext<ManagerContext>(ServiceLifetime.Transient)
                .AddScoped<SetContextConfig>()
                .AddDbContext<SetContext>(ServiceLifetime.Transient)
                ;
        }
        protected override void RegisterViewModel(IServiceCollection services)
        {
            base.RegisterViewModel(services);
            services
                .AddSingleton<VmMain>()
                .AddSingleton<VmSignIn>()
                ;
        }
    }
}
