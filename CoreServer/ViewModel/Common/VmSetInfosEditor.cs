using Core.Abstract.ViewModel;
using Core.Misc.EF.Manager;
using Core.Model.Sys;

namespace CoreServer.ViewModel.Common
{
    public class VmSetInfosEditor : VmDictionaryModelsEditor<ManagerContext, SetInfo>
    {
        protected override bool Search(SetInfo item, string value)
        {
            return base.Search(item, value) || item.Name.Contains(value);
        }
    }
}
