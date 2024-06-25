using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Misc.DI;
using Core.Misc.EF.Manager;
using Core.ViewModel.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServer.ViewModel.Common
{
    public class VmSignIn : ObservableObject
    {
        public IVmMessage VmMessage { get; set; }

        private string server = string.Empty;
        public string Server
        {
            get => server;
            set
            {
                SetProperty(ref server, value);
                CmdSignIn.NotifyCanExecuteChanged();
            }
        }

        private string user = string.Empty;
        public string User
        {
            get => user;
            set
            {
                SetProperty(ref user, value);
                CmdSignIn.NotifyCanExecuteChanged();
            }
        }

        private string password = string.Empty;
        public string Password
        {
            get => password;
            set
            {
                SetProperty(ref password, value);
                CmdSignIn.NotifyCanExecuteChanged();
            }
        }

        public AsyncRelayCommand CmdSignIn { get; set; }
        public RelayCommand CmdQuit { get; set; }

        public VmSignIn(IVmMessage vmMessage)
        {
            VmMessage = vmMessage;
            CmdSignIn = new(SignIn, CanSignIn);
            CmdQuit = new(Quit);
        }

        public async Task SignIn()
        {
            await VmMessage.StartWaiting("登录中").ConfigureAwait(false);
            ManagerContextConfig managerContextConfig = DIService.ServiceProvider.GetRequiredService<ManagerContextConfig>();
            managerContextConfig.Set(Server, User, Password);
            ManagerContext managerContext = DIService.ServiceProvider.GetRequiredService<ManagerContext>();
            try
            {
                bool canConnect = await managerContext.Database.CanConnectAsync();
                if (canConnect)
                {
                    if (managerContext.Database.HasPendingModelChanges())
                    {
                        await VmMessage.StartWaiting("正在更新服务");
                        await managerContext.Database.MigrateAsync();
                    }
                    DIService.ServiceProvider.GetRequiredService<IVmMain>().SwitchAppState(IVmMain.AppStates.Working);
                }
                else
                {
                    await VmMessage.StartWaiting("正在创建服务");
                    await managerContext.Database.MigrateAsync();
                    DIService.ServiceProvider.GetRequiredService<IVmMain>().SwitchAppState(IVmMain.AppStates.Working);
                }
            }
            catch (Exception ex)
            {
                await VmMessage.ShowError(ex);
            }
            finally
            {
                await VmMessage.StopWaiting();
            }
        }
        private bool CanSignIn()
        {
            return !(string.IsNullOrEmpty(Server) || string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password));
        }

        public static void Quit()
        {
            Environment.Exit(0);
        }
    }
}
