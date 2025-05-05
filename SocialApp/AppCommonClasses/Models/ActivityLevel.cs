using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCommonClasses.Models
{
    [Table("activity_levels")]
    public class ActivityLevel
    {
        [Key]
        [Column("al_id")]
        public int Id { get; set; }

        [Required]
        [Column("al_description")]
        public string Description { get; set; }
    }
} 