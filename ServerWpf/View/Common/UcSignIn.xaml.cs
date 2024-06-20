using Core.Misc.DI;
using CoreServer.ViewModel.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace ServerWpf.View.Common
{
    /// <summary>
    /// Interaction logic for UcSignIn.xaml
    /// </summary>
    public partial class UcSignIn : UserControl
    {
        private VmSignIn VmSignIn { get; set; }

        public UcSignIn()
        {
            InitializeComponent();
            VmSignIn = DIService.ServiceProvider.GetRequiredService<VmSignIn>();
            DataContext = VmSignIn;
        }

        private void PwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            VmSignIn.Password = PwdBox.Password;
        }
    }
}
