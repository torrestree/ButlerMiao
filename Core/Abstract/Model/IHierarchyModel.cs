using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Abstract.Model
{
    public interface IHierarchyModel<TSelf> : IDictionaryModel
    {
        int? ParentId { get; set; }
        TSelf? Parent { get; set; }
        ObservableCollection<TSelf> Children { get; set; }
    }

    public abstract class HierarchyModelBase<TSelf> : DictionaryModelBase, IHierarchyModel<TSelf>
    {
        private int? parentId;
        public int? ParentId
        {
            get => parentId;
            set => SetProperty(ref parentId, value);
        }

        private TSelf? parent;
        [ForeignKey(nameof(ParentId))]
        public TSelf? Parent
        {
            get => parent;
            set => SetProperty(ref parent, value);
        }

        private ObservableCollection<TSelf> children = [];
        public ObservableCollection<TSelf> Children
        {
            get => children;
            set => SetProperty(ref children, value);
        }
    }
}
