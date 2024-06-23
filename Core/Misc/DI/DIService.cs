using Microsoft.Extensions.DependencyInjection;

namespace Core.Misc.DI
{
    public static class DIService
    {
        public static IServiceProvider ServiceProvider { get; set; } = null!;

        public static void Register(IDIRegister register)
        {
            ServiceProvider = register.Register(new ServiceCollection()).BuildServiceProvider();
        }
    }
}
