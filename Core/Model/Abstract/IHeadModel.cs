using System.Collections.ObjectModel;

namespace Core.Model.Abstract
{
    public interface IHeadModel<TChild> : IDictionaryModel
    {
        ObservableCollection<TChild> Children { get; set; }
    }

    public abstract class HeadModel<TChild> : DictionaryModel, IHeadModel<TChild>
    {
        private ObservableCollection<TChild> children = null!;
        public virtual ObservableCollection<TChild> Children
        {
            get => children;
            set => SetProperty(ref children, value);
        }
    }
}
