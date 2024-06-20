using CommunityToolkit.Mvvm.ComponentModel;
using Core.Abstract.Model;
using System.ComponentModel.DataAnnotations;

namespace Core.Model.Sys
{
	public class SetInfo : DictionaryModelBase
	{
		private string name = string.Empty;
		public string Name
		{
			get => name;
			set => SetProperty(ref name, value);
		}

		private string server = string.Empty;
		public string Server
		{
			get => server;
			set => SetProperty(ref server, value);
		}

		private string user = string.Empty;
		public string User
		{
			get => user;
			set => SetProperty(ref user, value);
		}

		private string password = string.Empty;
		public string Password
		{
			get => password;
			set => SetProperty(ref password, value);
		}

		private int port;
		public int Port
		{
			get => port;
			set => SetProperty(ref port, value);
		}
	}
}
