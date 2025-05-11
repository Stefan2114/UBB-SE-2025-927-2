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
        public string Name { get; set; }
        public string? Image { get; set; }
        public string Description { get; set; }
        public long AdminId { get; set; }
    }
}
