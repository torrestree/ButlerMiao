using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Abstract.Model
{
    public interface IMiddleModel<TParent, TChild> : IDictionaryModel
    {
        int ParentId { get; set; }
        TParent Parent { get; set; }
        ObservableCollection<TChild> Children { get; set; }
    }

    public abstract class MiddleModelBase<TParent, TChild>(TParent parent) : DictionaryModelBase, IMiddleModel<TParent, TChild>
    {
        private int parentId;
        public int ParentId
        {
            get => parentId;
            set => SetProperty(ref parentId, value);
        }

        private TParent parent = parent;
        [ForeignKey(nameof(ParentId))]
        public TParent Parent
        {
            get => parent;
            set => SetProperty(ref parent, value);
        }

        private ObservableCollection<TChild> children = [];
        public ObservableCollection<TChild> Children
        {
            get => children;
            set => SetProperty(ref children, value);
        }
    }
}
