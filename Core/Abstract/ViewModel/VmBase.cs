using CommunityToolkit.Mvvm.ComponentModel;
using Core.Misc.DI;
using Core.ViewModel.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Abstract.ViewModel
{
    public abstract class VmBase : ObservableObject
    {
        public VmMessage VmMessage { get; set; }

        protected VmBase()
        {
            VmMessage = DIService.ServiceProvider.GetRequiredService<VmMessage>();
        }
    }
}
