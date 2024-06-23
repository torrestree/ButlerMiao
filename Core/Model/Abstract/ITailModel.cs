using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Model.Abstract
{
    public interface ITailModel<TParent> : IDictionaryModel
    {
        int ParentId { get; set; }
        TParent Parent { get; set; }
    }

    public abstract class TailModel<TParent>(TParent parent) : DictionaryModel, ITailModel<TParent>
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
    }
}
