using System.Collections.ObjectModel;

namespace Core.Abstract.Model
{
    public interface IHeadModel<TChild> : IDictionaryModel
    {
        ObservableCollection<TChild> Children { get; set; }
    }

    public abstract class HeadModelBase<TChild> : DictionaryModelBase, IHeadModel<TChild>
    {
        private ObservableCollection<TChild> children = [];
        public ObservableCollection<TChild> Children
        {
            get => children;
            set => SetProperty(ref children, value);
        }
    }
}
