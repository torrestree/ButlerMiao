using Core.Misc.DI;
using CoreServer.ViewModel.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace ServerWpf.View.Common.SignIn
{
    /// <summary>
    /// Interaction logic for ViewSignIn.xaml
    /// </summary>
    public partial class ViewSignIn : UserControl
    {
        public ViewSignIn()
        {
            InitializeComponent();
            DataContext = DIService.ServiceProvider.GetRequiredService<VmSignIn>();
        }
    }
}
