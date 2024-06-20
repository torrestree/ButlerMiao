using Core.Misc.DI;
using CoreServer.ViewModel.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace ServerWpf.View.Common
{
    /// <summary>
    /// Interaction logic for UcSetInfosEditor.xaml
    /// </summary>
    public partial class UcSetInfosEditor : UserControl
    {
        public UcSetInfosEditor()
        {
            InitializeComponent();
            DataContext = DIService.ServiceProvider.GetRequiredService<VmSetInfosEditor>();
        }
    }
}
