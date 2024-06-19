using Microsoft.Extensions.DependencyInjection;

namespace Core.Misc.DI
{
    public interface IDIRegister
    {
        IServiceCollection Register(IServiceCollection services);
    }
}
