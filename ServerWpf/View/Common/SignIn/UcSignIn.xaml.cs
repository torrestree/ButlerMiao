using CoreServer.ViewModel.Common;
using System.Windows;
using System.Windows.Controls;

namespace ServerWpf.View.Common.SignIn
{
    /// <summary>
    /// Interaction logic for UcSignIn.xaml
    /// </summary>
    public partial class UcSignIn : UserControl
    {
        public UcSignIn()
        {
            InitializeComponent();
        }

        private void PwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((VmSignIn)DataContext).Password = PwdBox.Password;
        }
    }
}
