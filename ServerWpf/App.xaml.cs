using Core.Misc.DI;
using ServerWpf.Misc.DI;
using System.Windows;

namespace ServerWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DIService.Register(new DIRegister());
        }
    }
}
