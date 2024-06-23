using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Model.Abstract
{
    public interface IMiddleModel<TParent, TChild> : IDictionaryModel
    {
        int ParentId { get; set; }
        TParent Parent { get; set; }
        ObservableCollection<TChild> Children { get; set; }
    }

    public abstract class MiddleModel<TParent, TChild>(TParent parent) : DictionaryModel, IMiddleModel<TParent, TChild>
    {
        private int parentId;
        public int ParentId
        {
            get => parentId;
            set => SetProperty(ref parentId, value);
        }

        private TParent parent = parent;
        [ForeignKey(nameof(ParentId))]
        public virtual TParent Parent
        {
            get => parent;
            set => SetProperty(ref parent, value);
        }

        private ObservableCollection<TChild> children = [];
        public virtual ObservableCollection<TChild> Children
        {
            get => children;
            set => SetProperty(ref children, value);
        }
    }
}
