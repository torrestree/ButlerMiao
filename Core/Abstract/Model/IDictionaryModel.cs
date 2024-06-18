using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Abstract.Model
{
    public interface IDictionaryModel
    {
        int Id { get; set; }
    }

    public abstract class DictionaryModelBase : ObservableObject, IDictionaryModel
    {
        private int id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
    }
}
