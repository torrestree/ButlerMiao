using Core.Misc.DI;
using CoreServer.ViewModel.Common;
using Microsoft.Extensions.DependencyInjection;
using ServerWpf.View.Common.SignIn;
using ServerWpf.View.Common.Workspace;

namespace ServerWpf.ViewModel
{
    public class VmMain : IVmMain
    {
        public MainWindow MainWindow { get; set; } = null!;

        public void SwitchAppState(IVmMain.AppStates appState)
        {
            MainWindow.Content = appState switch
            {
                IVmMain.AppStates.SignIn => DIService.ServiceProvider.GetRequiredService<ViewSignIn>(),
                IVmMain.AppStates.Working => DIService.ServiceProvider.GetRequiredService<ViewWorkspace>(),
                _ => null,
            };
        }
    }
}
