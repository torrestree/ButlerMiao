using Core.Misc.DI;
using CoreServer.ViewModel.Common;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServer.Misc.DI
{
    public abstract class DIRegisterCoreServer : DIRegisterCore
    {
        protected override void RegisterViewModel(IServiceCollection services)
        {
            base.RegisterViewModel(services);
            services
                .AddTransient<VmSignIn>()
                ;
        }
    }
}
