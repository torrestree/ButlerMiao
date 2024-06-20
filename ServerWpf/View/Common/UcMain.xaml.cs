using Core.Misc.DI;
using CoreServer.ViewModel.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace ServerWpf.View.Common
{
    /// <summary>
    /// Interaction logic for UcMain.xaml
    /// </summary>
    public partial class UcMain : UserControl
    {
        public UcMain()
        {
            InitializeComponent();
            DataContext = DIService.ServiceProvider.GetRequiredService<VmMain>();
        }
    }
}
