using Core.Model.Sys;
using Core.ViewModel.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Misc.DI
{
    public abstract class DIRegisterCore : IDIRegister
    {
        public IServiceCollection Register(IServiceCollection services)
        {
            RegisterMisc(services);
            RegisterModel(services);
            RegisterViewModel(services);
            RegisterView(services);
            return services;
        }
        protected virtual void RegisterMisc(IServiceCollection services)
        {

        }
        protected virtual void RegisterModel(IServiceCollection services)
        {
            services
                .AddTransient<SetInfo>()
                ;
        }
        protected virtual void RegisterViewModel(IServiceCollection services)
        {
            services
                .AddTransient<VmMessage>()
                ;
        }
        protected virtual void RegisterView(IServiceCollection services) { }
    }
}
