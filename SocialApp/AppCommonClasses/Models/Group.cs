using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCommonClasses.Models
{
    public class Group
    {
        [Key]
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string Image { get; set; }
        public required string Description { get; set; }
        public required long AdminId { get; set; }
    }
}
