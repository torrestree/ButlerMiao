using Core.Misc.DI;
using HandyControl.Controls;
using Microsoft.Extensions.DependencyInjection;
using ServerWpf.View.Common;
using System.Windows.Controls;

namespace ServerWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : GlowWindow
    {
        private UserControl? UserControl { get; set; }
        private bool AppState { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            UserControl = null;
            if (AppState)
            {
                
                UserControl = DIService.ServiceProvider.GetRequiredService<UcMain>();
                
            }
            AddChild(UserControl);
        }
    }
}