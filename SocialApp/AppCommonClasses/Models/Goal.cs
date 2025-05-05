using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCommonClasses.Models
{
    [Table("goals")]
    public class Goal
    {
        [Key]
        [Column("g_id")]
        public int Id { get; set; }

        [Required]
        [Column("g_description")]
        public string Description { get; set; }
    }
} 