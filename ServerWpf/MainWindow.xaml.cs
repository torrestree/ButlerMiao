using Core.Misc.DI;
using CoreServer.ViewModel.Common;
using HandyControl.Controls;
using Microsoft.Extensions.DependencyInjection;
using ServerWpf.ViewModel;
using System.Windows;

namespace ServerWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : GlowWindow
    {
        private VmMain VmMain { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            VmMain = (VmMain)DIService.ServiceProvider.GetRequiredService<IVmMain>();
            VmMain.MainWindow = this;
            DataContext = VmMain;
            VmMain.SwitchAppState(IVmMain.AppStates.SignIn);
        }
    }
}