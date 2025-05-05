using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCommonClasses.Models
{
    [Table("allergies")]
    public class Allergy
    {
        [Key]
        [Column("a_id")]
        public int Id { get; set; }

        [Required]
        [Column("a_description")]
        public string Description { get; set; }
    }
} 