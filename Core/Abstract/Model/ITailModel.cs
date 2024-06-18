namespace Core.Abstract.Model
{
    public interface ITailModel<TParent> : IDictionaryModel
    {
        int ParentId { get; set; }
        TParent Parent { get; set; }
    }

    public abstract class TailModelBase<TParent>(TParent parent) : DictionaryModelBase, ITailModel<TParent>
    {
        private int parentId;
        public int ParentId
        {
            get => parentId;
            set => SetProperty(ref parentId, value);
        }

        private TParent parent = parent;
        public TParent Parent
        {
            get => parent;
            set => SetProperty(ref parent, value);
        }
    }
}
