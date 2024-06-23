using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Model.Abstract
{
    public interface IDictionaryModel
    {
        int Id { get; set; }
    }

    public abstract class DictionaryModel : ObservableObject, IDictionaryModel
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
