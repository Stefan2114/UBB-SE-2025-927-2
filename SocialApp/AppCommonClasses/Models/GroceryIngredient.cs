using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AppCommonClasses.Models
{
    [Table("grocery_lists")]
    public class GroceryIngredient : ObservableObject
    {
        [Column("u_id")]
        [JsonIgnore]
        public long Id { get; set; }

        [Column("i_id")]
        public int IngredientId { get; set; }

        private string _name = string.Empty;

        [NotMapped]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private bool _isChecked;
        [Column("is_checked")]
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        public readonly static GroceryIngredient defaultIngredient = new GroceryIngredient() { Id = -1, _name = "", IsChecked = false };
    }
}
