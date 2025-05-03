using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppCommonClasses.Models
{
    public class ServingUnitModel
    {
        [Key]
        [Column("unit_name")]
        public string? UnitName { get; set; } // The unit name from the database
    }
}
