using CommunityToolkit.Mvvm.Input;
using Core.Abstract.ViewModel;
using Core.Misc.DI;
using Core.Misc.EF.Manager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServer.ViewModel.Common
{
	public class VmSignIn : VmBase
	{
		private bool isVisible = true;
		public bool IsVisible
		{
			get => isVisible;
			set => SetProperty(ref isVisible, value);
		}

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

		public VmSignIn()
		{
			CmdSignIn = new(SignIn, CanSignIn);
			CmdQuit = new(Quit);
		}

		public async Task SignIn()
		{
			await VmMessage.ShowWaiting("登录中").ConfigureAwait(false);
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
						VmMessage.Content = "正在迁移服务数据";
						await managerContext.Database.MigrateAsync().ConfigureAwait(false);
					}
					IsVisible = false;
				}
				else
				{
					VmMessage.Content = "正在创建服务数据";
					await managerContext.Database.MigrateAsync().ConfigureAwait(false);
					IsVisible = false;
				}
			}
			catch (Exception ex)
			{
				await VmMessage.ShowError(ex);
			}
			finally
			{
				await VmMessage.CloseWaiting();
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
